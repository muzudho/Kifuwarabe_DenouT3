-- A.I.M.S. 用汎用関数群ファイル

--------------------------------------------------------------------------------
-- 定義定数一覧


-- addMover()で使うアクション定数

MOVER_SETANGLE=1		 	-- 方角指定
MOVER_SETSPEED=2		 	-- 速度指定
MOVER_TURN=3			 	-- 旋回
MOVER_ACCEL=4			 	-- 加減速
MOVER_SETFACE=5			 	-- キャラクタ切り替え
MOVER_SETPOSITION=6		 	-- 座標指定
MOVER_SETPOS=6			 	-- 座標指定
MOVER_SETVECTOR=7		 	-- ベクトル指定
MOVER_ADDVECTOR=8		 	-- ベクトル加算
MOVER_SETHITRADIUS=9	 	-- 当たり判定サイズ変更
MOVER_SETZOOM=10		 	-- 拡大率設定
MOVER_ADDZOOM=11		 	-- 拡大率加減算
MOVER_SETBLEND=12		 	-- 描画時のブレンド法変更
MOVER_SETALPHA=13		 	-- α値定義
MOVER_ADDALPHA=14		 	-- α値加減算
MOVER_SETIMMORTALTIME=15 	-- 無敵(壁抜け)時間の設定
MOVER_SETUNHARMFULTIME=16	-- 無害時間の設定
MOVER_VANISH=17				-- 消滅指示
MOVER_LASEREFFECTFLAG=18	-- レーザーにのみ適用可能。レーザーの根元のエフェクトの設定
MOVER_SETWAIT=19			-- 処理停止時間の設定
MOVER_SETANIMEPLAY=20		-- アニメ動作フラグ
MOVER_SETHEADING=21			-- 見た目方角指定
MOVER_TURNHEADING=22		-- 見た目旋回
MOVER_SETHEADSYNC=23		-- 見た目と移動方角の同期設定
MOVER_SETCOLOR=24			-- 色設定
MOVER_SETZ=25				-- Z値設定
MOVER_MOVETO_L=26			-- 指定座標へ移動(線形)
MOVER_MOVETO_A=27			-- 指定座標へ移動(加速)
MOVER_MOVETO_B=28			-- 指定座標へ移動(ブレーキ)
MOVER_MOVETO_C=29			-- 指定座標へ移動(両方)
MOVER_ZOOMTO_L=30			-- 指定拡大率に変化(線形)
MOVER_ZOOMTO_A=31			-- 指定拡大率に変化(加速)
MOVER_ZOOMTO_B=32			-- 指定拡大率に変化(ブレーキ)
MOVER_ZOOMTO_C=33			-- 指定拡大率に変化(両方)
MOVER_INVERT_OBCHECK=34		-- 画面外判定処理をレイヤー設定と逆にする
			-- (自動消滅設定のあるレイヤーだとOFF、ないレイヤーだとON)

-- MOVER_CHANGEBLEND などで valueに与えるブレンディング定数

BLEND_NONE=0    	-- アルファブレンド無効
BLEND_NORMAL=1		-- 通常ブレンディング
BLEND_ADD=2     	-- 加算ブレンディング
BLEND_MUL=3     	-- 乗算ブレンディング
BLEND_REVERSE=4 	-- 反転ブレンディング

-- OnVanish の引数で与えられる消去原因の値

KILL_ORDERED=0		-- スクリプトorMoverで消去を指示された
KILL_ALLDELETE=1	-- 
KILL_OUTOFSCREEN=2	-- 画面外に出て自動消去が働いた

-- isJoyPressedの引数に与えるボタン定数

BUTTON_RIGHT = 0
BUTTON_DOWN  = 1
BUTTON_LEFT  = 2
BUTTON_UP    = 3
BUTTON_TRIG1 = 4
BUTTON_TRIG2 = 5
BUTTON_TRIG3 = 6
BUTTON_TRIG4 = 7
BUTTON_TRIG5 = 8
BUTTON_TRIG6 = 9
BUTTON_TRIG7 = 10
BUTTON_TRIG8 = 11
BUTTON_TRIG9 = 12
BUTTON_TRIG10= 13
BUTTON_TRIG11= 14
BUTTON_TRIG12= 15

MOUSE_LEFT    = 0
MOUSE_RIGHT   = 1
MOUSE_CENTER  = 2
MOUSE_BUTTON4 = 3
MOUSE_BUTTON5 = 4

-- setGraphicCenterの座標指定の対象

GCENTER_ZOOM = 0 		-- 拡大のみ
GCENTER_ROTATION = 1 	-- 回転のみ
GCENTER_BOTH = 2 		-- 両方(これが規定値)

--------------------------------------------------------------------------------
--[[定義関数一覧

	リソース関連
setLayerSetting(layer_params)
setGraphicAnimeLoop (himages,delay,looppoint)
setGraphicAnimeSequence (himages,delay)
deleteAllGraphic(g)
setAllGraphicHit(g,a,b,c,d)
deleteAllSound(s)
stopSounds(s)

	Actorの(ang,speed)-Vectorの変換命令
makevector(ang,spd)
getFromVector(actor)
getAngleFromVector(actor)
getSpeedFromVector(actor)

	Actor動作
inbound(uID,x,y,wid,hei)
inbound2(uID,x,y,x2,y2)
move_invprop(px,py,a,limit)
move_invprop_id(uID,px,py,a,limit)
rotateTo(angle,delta)

	ある座標からある座標を狙う場合の数値計算
aimNPlayerX(X1,Y1,X2,Y2)
aimStoP(x,y)
distPtoP(x1,y1,x2,y2)
distStoP(x,y)

	パッド入力検出
ojInit()
ojStep()
ojGet(id)

	あるActorを中心に対称Actorを回転させる命令　取得にはaimやdistを使う
setAltPosLotDeg(uidMaster,uidSlave,x,y)
setAltPosLotVec(uidMaster,uidSlave,x,y)
setAltPos(uidMaster,uidSlave,x,y)

	ステージ制御関連
HideTimerGauge()
wait(n)
inKey()

	Actorの残像を作成
createAura(time,startAlpha,Blend,layer)

]]
--------------------------------------------------------------------------------



-- 以下ユーティリティ関数

----------------------------------------
-- 
----------------------------------------

function setLayerSetting(layer_params)
 local i,p
 for i=0,11,1 do
  p=i*4+1
  setLayerAutoVanish(i,layer_params[p])
  setLayerUseFastDraw(i,layer_params[p+1])
  if layer_params[p+1]==true then
   setLayerUseAutoYSort(i,layer_params[p+2])
   setLayerGraphicForFastDraw(i,layer_params[p+3])
  end
 end
end

function setGraphicAnimeLoop (himages,delay,looppoint)
  if(type(himages) ~= "table") then
	error("setGraphicAnimeLoop:第一引数はテーブルでなければなりません。",2);
  end
  local i=0
  for i=1,#(himages),1 do
    if i==(#(himages)) then
      setGraphicAnime(himages[i],himages[looppoint],delay)
    else
      setGraphicAnime(himages[i],himages[i+1],delay)
    end
  end
  return himages[1]
end

function setGraphicAnimeSequence (himages,delay)
  if(type(himages) ~= "table") then
	error("setGraphicAnimeLoop:第一引数はテーブルでなければなりません。",2);
  end
  local i=0
  for i=1,#(himages),1 do
    if i==(#(himages)) then
      setGraphicAnime(himages[i],-1,delay)
    else
      setGraphicAnime(himages[i],himages[i+1],delay)
    end
  end
  return himages[1]
end

--
-- テーブル g 内の画像を全消去。
--
function deleteAllGraphic(g)
  local hchip;
  for index, hchip in pairs(g) do
    if(type (hchip) == "table") then
      deleteAllGraphic(hchip)
    else
      deleteGraphic(hchip)
    end
  end
  g = {};
end

--
-- テーブル g 内のグラフィックの当たり判定を一括設定
--
function setAllGraphicHit(g,a,b,c,d)
	if type(g) == "table" then
		for index, hchip in pairs(g) do
			if(type (hchip) == "table") then
				setAllGraphicHit(hchip,a,b,c,d);
			else
				addGraphicHit(hchip,a,b,c,d);
			end
		end
		g = {};
	end
end

--
-- テーブル s 内の音声を全消去。
--
function deleteAllSound(s)
	if type(s) == "table" then
		for index, hchip in pairs(s) do
			if(type (hchip) == "table") then
				deleteAllSound(hchip)
			else
				deleteSound(hchip)
			end
		end
		s = {};
	end
end

function stopSounds(s)
	if type(s) == "table" then
		for index, hchip in pairs(s) do
			if(type (hchip) == "table") then
				stopSounds(hchip)
			else
				stopSound(hchip)
			end
		end
	end
end

--
-- ang,spdからベクトル生成。戻り値2個。
--
function makevector(ang,spd)
  local x,y
  x=math.cos(math.rad(ang))*spd
  y=math.sin(math.rad(ang))*spd
  return x,y
end

--
-- ベクトルからang,spdを取得
-- 存在しない場合はnilが返るお
--
function getFromVector(actor)
  if actor == nil then  actor = iSelf()  end
  if isAlive(actor) == false then  return nil  end
  
  local vx, vy, ang, spd
  vx=getVX(actor)
  vy=getVY(actor)
  
  -- 角度取得
  ang=math.deg(math.atan(vy/vx))
  if vx < 0 and vy > 0 then  ang = ang + 180  end	-- 第2象限
  if vx < 0 and vy <= 0 then  ang = ang + 180  end	-- 第3象限
  if vx >= 0 and vy < 0 then  ang = ang + 360  end	-- 第4象限
  
  -- 速度取得
  spd = math.sqrt((vx*vx) + (vy*vy))
  
  return ang,spd
end

function getAngleFromVector(actor)
  if actor == nil then  actor = iSelf()  end
  
  local ang,spd = getFromVector(actor)
  return ang
end

function getSpeedFromVector(actor)
  if actor == nil then  actor = iSelf()  end
  
  local ang,spd = getFromVector(actor)
  return spd
end

--

function inbound(uID,x,y,wid,hei)
  local px=getX(uID)
  local py=getY(uID)

  if(px<x) then
    return false
  end
  if(px>x+wid) then
    return false
  end
  if(py<y) then
    return false
  end
  if(py>y+hei) then
    return false
  end
  return true
end

function inbound2(uID,x,y,x2,y2)
  local px=getX(uID)
  local py=getY(uID)

  if(px<x) then
    return false
  end
  if(px>x2) then
    return false
  end
  if(py<y) then
    return false
  end
  if(py>y2) then
    return false
  end
  return true
end

function rotateTo(angle,delta)
  local dl=0
  local dr=0
  local ret=false

  dl=angle-getAngle()
  if dl<0 then
    dl=0-dl
    dr=360-dl
    if (dl>dr and dr<=delta) or (dl<dr and dl<=delta) then
      --1フレームで届くなら書き換え、ダメなら回転
      setAngle(angle)
      ret=true
    else
      if dr<dl then
        turn(delta)
      else
        turn(-delta)
      end
    end
  else
    dr=360-dl
    if (dl>dr and dr<=delta) or (dl<dr and dl<=delta) then
      --1フレームで届くなら書き換え、ダメなら回転
      setAngle(angle)
      ret=true
    else
      if dr>dl then
        turn(delta)
      else
        turn(-delta)
      end
    end
  end
  return ret
end

--引数を4つ取る型。4つの引数からaimする。
function aimPtoP(X1,Y1,X2,Y2)
  local AimRad = -math.atan2(X2 - X1 , Y2 - Y1)
  return math.deg(AimRad)+90
end

function aimStoP(x,y)
  return aimPtoP(getX(),getY(),x,y)
end

function distPtoP(x1,y1,x2,y2)
  return math.sqrt(math.pow(x2-x1,2)+math.pow(y2-y1,2))
end

-- 呼び出し元キャラから指定地点への距離を返す。

function distStoP(x,y)
  return distPtoP(getX(),getY(),x,y)
end



function _common_OnLoad()
end

function _common_OnVanish()
end

function debugString(str, printable)
	if(printable==nil)then printable = true end
	debugOut(tostring( str ), printable);
end

function dm(str, printable)
	debugString(str, printable);
end

--------------------------------------------------------------------------------
-- wait(n)
--------------------------------------------------------------------------------

-- スレッドルーチン内で、一定時間idleする
-- startThread("functionname") によって起動される関数内で使用する事
-- 引数：1
	-- n : 待機時間(frame)
-- 返値：0


function wait(n)
	for i = 1,n,1 do
		coroutine.yield()
	end
end

--------------------------------------------------------------------------------
-- printnode(n)
--------------------------------------------------------------------------------

-- nに指定したテーブルを丸々debugOutする。

function printnode(n, indent)
	if(indent == nil)then
		indent = 0;
	end
	if indent == 0 then
		debugOut("printnode: {");
	end
	if type(n) == "table" then
		for k , v in pairs(n) do
			if(type (v) == "table") then
				debugOut(string.rep("　", indent)..k.." = {");
				printnode(v, indent+2);
				debugOut(string.rep("　", indent).."}");
			elseif type(v) == "string" then
				debugOut(string.rep("　", indent)..k.." = \""..v.."\"");
			else
				debugOut(string.rep("　", indent)..k.." = "..tostring(v));
			end
		end
	end
	if indent == 0 then
		debugOut("}");
	end
end


--------------------------------------------------------------------------------
-- serialize(n)
--------------------------------------------------------------------------------

-- nに指定したテーブルを丸々ひとつの文章にする。

function serialize(n, indent)
	if(n == nil)then
		return nil;
	end
	
	local rtn = "";
	local table_code = "";
	local stack = {};
	if(indent == nil)then
		rtn = "N = {};\n";
		table_code = "N";
	else
		rtn = indent.." = {};\n";
		table_code = indent;
	end
	
	if type(n) == "table" then
		for k , v in pairs(n) do
			if(type (v) == "table") then
				-- テーブルなのでスタックして後で処理する
				
				-- 添え字が数字外かチェック
				if type(k) == "string" then
					k = "\""..k.."\"";
				end
				k = table_code .. "["..k.."]";
				table.insert(stack, {value=v, index=k});
			else
				-- 添え字があるなら準備する
				if k ~= nil then
					if type(k) == "string" then
						k = "\""..k.."\"";
					end
					rtn = rtn .. table_code .. "["..k.."] = ";
				end
				
				-- 実際の値を記述
				if type(v) == "string" then
					v = "\""..v.."\"";
				end
				rtn = rtn .. v .. ";\n";
			end
		end
	end
	
	-- スタックしておいたテーブルを順番に処理していく
	for i=1,#stack,1 do
		rtn = rtn .. serialize(stack[i].value, stack[i].index);
	end

	-- 展開したデータを戻すためのreturnを記載
	if(indent == nil)then
		rtn = rtn.."return N;";
	end

	return rtn;
end


function deserialize(n)
	if(n == nil)then
		return "";
	end
	
	return assert(loadstring(n))()
end

--------------------------------------------------------------------------------
-- trig = getTrigID()
--------------------------------------------------------------------------------

-- 押されたボタンのIDに対応した文字列を返す。
_TRIG = {
		{ BUTTON_UP,	"up"	};
		{ BUTTON_DOWN,	"down"	};
		{ BUTTON_LEFT,	"left"	};
		{ BUTTON_RIGHT,	"right"	};
		{ BUTTON_TRIG1,	"trig1"	};
		{ BUTTON_TRIG2,	"trig2"	};
		{ BUTTON_TRIG3,	"trig3"	};
}
function getTrigID(PadID)
	if( PadID == nil )then
		PadID = 0;
	end
	
	for i=1,#(_TRIG) do
		if( getJoyPressCount(PadID, _TRIG[i][1]) == 1 )then
			return _TRIG[i][2];
		end
	end
	
	return nil;
	
end


