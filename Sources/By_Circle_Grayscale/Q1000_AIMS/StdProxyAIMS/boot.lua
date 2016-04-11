prequire("common.lua");

function OnLoad()
	PH = {};	-- プロセス管理テーブル(Process Handle)
end

function boot_OnStart()
	require 'StdProxyAIMS'; -- システムのロード

	-- StdProxyAIMS.StartProcess の 返り値 ＝ プロセス識別ハンドル
	-- ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
	-- ★本来はこのハンドルを使うことにより、同時にいくつものプロセスと通信できます。★
	-- ★　しかし、現時点では１つのプロセスの起動にしか対応していません。　　　　　　★
	-- ★　→複数アプリとの同時通信はできません。　　　　　　　　　　　　　　　　　　★
	-- ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

	PH.Sample = StdProxyAIMS.StartProcess("Sample.exe"); -- プロセスをSTD通信モード起動し、変数「PH.Handle」にハンドルを保存する。。

	--ここはAIMS固有
	font1 = createTextFont("Meiryo", 32)
	text1 = createBlankGraphic( 640, 64);
	createActor( text1, 320, 32, 11 );
	-- ここまで

	StdProxyAIMS.WriteData(PH.Sample, "This is Test Data!\n"); -- STDINに文字列送信
end

function boot_OnStep()
	R = StdProxyAIMS.ReadData(PH.Sample); -- STDOUTの文字列を取得
	if R then -- 受信された文字列がない場合は nil になるので

		--ここはAIMS固有
		fillGraphic(text1, 0, 0, 25, 255);
		drawTextToGraphic( text1, font1, 0, 0, R, 255, 255, 255, 255, false)
		-- ここまで

		sleep(300); -- 一瞬で文字が流れてしまうので遅くしてみる（実運用では無意味）
	end

end

function boot_OnClose()
end

function OnVanish()
	StdProxyAIMS.CloseProcess(PH.Sample);
end

