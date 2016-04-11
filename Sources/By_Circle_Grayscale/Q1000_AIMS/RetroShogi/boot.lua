prequire("common.lua");


-- グローバル変数
V = {}
V.state = "init" -- 通信の状態



function OnLoad()
	PH = {};	-- プロセス管理テーブル(Process Handle)
end


function boot_OnStart()
	require 'StdProxyAIMS' -- システムのロード

	-- StdProxyAIMS.StartProcess の 返り値 ＝ プロセス識別ハンドル
	-- ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
	-- ★本来はこのハンドルを使うことにより、同時にいくつものプロセスと通信できます。★
	-- ★　しかし、現時点では１つのプロセスの起動にしか対応していません。　　　　　　★
	-- ★　→複数アプリとの同時通信はできません。　　　　　　　　　　　　　　　　　　★
	-- ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

    -- プロセスをSTD通信モード起動し、変数「PH.Handle」にハンドルを保存する。
	PH.Sample = StdProxyAIMS.StartProcess("../../P050_KifuWarabe/KifuWarabe/bin/Release/Grayscale.P050_KifuWarabe.exe")
	--PH.Sample = StdProxyAIMS.StartProcess("./P200_KifuNarabe/KifuNarabe/bin/Release/Grayscale.P200_KifuNarabe.exe")
	--PH.Sample = StdProxyAIMS.StartProcess("./P400_KifuNarabeVs/KifuNarabeVs/bin/Release/Grayscale.P400_KifuNaraVs.exe")

	--ここはAIMS固有
	font1 = createTextFont("Meiryo", 32)
	text1 = createBlankGraphic( 640, 64)
	createActor( text1, 320, 32, 11 )
	-- ここまで

end

--
-- くるくる回っている☆ｗ
--
function boot_OnStep()

    --[[
    if V.state == "init" then

        -- 起動したプロセスに、文字列を送ります。（STDINに文字列送信）末尾の改行は必要。
	    StdProxyAIMS.WriteData(PH.Sample, "おいっ☆起きろ☆ｗｗ\n");
        V.state = "waitAwake"

    elseif V.state == "waitAwake" then

        -- 起動したプロセスから送られてくる文字列を監視します。（STDOUTの文字列を取得）
        receive = StdProxyAIMS.ReadData(PH.Sample);
        
        if receive then -- 受信された文字列がない場合は nil になるので弾ける☆

            --ここはAIMS固有
            fillGraphic(text1, 0, 0, 25, 255);
            drawTextToGraphic( text1, font1, 0, 0, receive, 255, 255, 255, 255, false)
            -- ここまで

        end

    end

    --sleep(300) -- 一瞬で文字が流れてしまうので遅くしてみる（実運用では無意味）
    ]]

end

function boot_OnClose()
end

function OnVanish()
	StdProxyAIMS.CloseProcess(PH.Sample);
end

