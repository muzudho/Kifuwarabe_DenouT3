prequire("common.lua");

function OnLoad()
end

function boot_OnStart()

	require 'CSProxyLUA';
	R = CSProxyLUA.Call("Sample.dll", "ProjectDark.KWSupport.LUASample", "HelloWorld");

	font1 = createTextFont("Meiryo", 32)
	text1 = createBlankGraphic( 640, 64);
	drawTextToGraphic( text1, font1, 0, 0, R, 255, 255, 255, 255, false)
	createActor( text1, 320, 32, 11 );

end

function boot_OnStep()
end

function boot_OnClose()
end

function OnVanish()
end

