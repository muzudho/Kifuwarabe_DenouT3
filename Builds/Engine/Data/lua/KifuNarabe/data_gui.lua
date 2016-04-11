-- クリアーボタンを押したとき
function click_clearButton()
    debugOut( "ふふー☆ｗ" )
    kifu_clear()
    screen_refreshStarlights()
    outputBox_clear()
    screen_refresh()
end

-- 再生ボタンを押したとき
function click_playButton()
    debugOut( "プレイボタン☆ｗ" )
    inputBox_play()
end
