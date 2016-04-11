/**
 * グラフィカル局面ログ
 *
 *     これを使って、将棋エンジンのログをグラフィカルにしたいんだぜ～☆
 *     マスに色が塗れて、矢印を引けるのがチャームポイントなんだぜ～☆v(^▽^)v
 *
 * 制  作：むずでょ（サークル  ぐれーすけーる）
 */

var g_boardNumber = 0;

/**
 * 2D描画
 */
var canvas;
var ctxBG;      //マス用（背景）
var ctxMAIN;    //いつも用
var ctxARW;     //矢印用

/**
 * 201マスのデータ
 */
var masus_x_arr = new Array();
var masus_y_arr = new Array();
var masu_w = 22;
var masu_h = 22;
var masu_spW = 3; //すきま
var masu_spH = 3;



        /**
         * データ構造を渡してください。
         * あとは自動で　局面を描画します。
         *
         * 複数形の s だぜ☆
         */
        function drawBoards(data_arr)
        {
            createAnnounce1();
            
            var i;
            for( i=0; i<data_arr.length; i++ )
            {
                drawBoard(data_arr[i]);
            }
        }
        
        /**
         * 局面１個分だぜ☆
         *
         * 複数形の s は付いてないぜ☆
         */
        function drawBoard( data)
        {
            // キャンバスは３枚重ねています。

            //
            // キャンバス「board(数字)_background」
            //
            var background_x;
            var background_y;
            {
                var canvasBG = document.createElement("canvas");
                canvasBG.setAttribute( "id", "board"+g_boardNumber+"_background" );
                
                canvasBG.setAttribute( "left", "0" );
                canvasBG.setAttribute( "top", "0" );
                canvasBG.setAttribute( "width", "590" );
                canvasBG.setAttribute( "height", "290" );
                canvasBG.setAttribute( "style", "border:solid 1px black;" );
                document.body.appendChild( canvasBG );
                
                if ( ! canvasBG || ! canvasBG.getContext ) { return false; }
                ctxBG = canvasBG.getContext('2d');
                
                makeBoard();

                background_x = canvasBG.offsetLeft;
                background_y = canvasBG.offsetTop;
            }

            //
            // キャンバス「board(数字)」
            //
            var canvas = document.createElement("canvas");
            canvas.setAttribute( "id", "board"+g_boardNumber );
            canvas.setAttribute( "width", "590" );
            canvas.setAttribute( "height", "290" );
            canvas.setAttribute( "style", "border:solid 1px black; position:absolute; left:"+background_x+"px; top:"+background_y+"px;" );
            document.body.appendChild( canvas );
            if ( ! canvas || ! canvas.getContext ) { return false; }
            ctxMAIN = canvas.getContext('2d');

            //
            // キャンバス「board(数字)_arrow」
            //
            var canvasARW = document.createElement("canvas");
            canvasARW.setAttribute( "id", "board"+g_boardNumber+"_arrow" );
            canvasARW.setAttribute( "width", "590" );
            canvasARW.setAttribute( "height", "290" );
            canvasARW.setAttribute( "style", "border:solid 1px black; position:absolute; left:"+background_x+"px; top:"+background_y+"px;" );
            document.body.appendChild( canvasARW );
            if ( ! canvasARW || ! canvasARW.getContext ) { return false; }
            ctxARW = canvasARW.getContext('2d');



            

        
    
            var i;
            for(i=0; i<data.length; i++)
            {
                if(data[i].act=="drawImg")
                {
                    drawImg( data[i].img, data[i].masu );
                }
                else if(data[i].act=="drawMasu")
                {
                    drawMasu( data[i].masu );
                }
                else if(data[i].act=="colorMasu")
                {
                    colorMasu( data[i].style );
                }
                else if(data[i].act=="colorArrow")
                {
                    colorArrow( data[i].style );
                }
                else if(data[i].act=="drawArrow")
                {
                    drawArrow( data[i].from, data[i].to );
                }
                else if(data[i].act=="drawText")
                {
                    drawText( data[i].text, data[i].x, data[i].y );
                }
                else if(data[i].act=="begin_imgColor")
                {
                    begin_imgColor( data[i].colors );
                }
                else if(data[i].act=="end_imgColor")
                {
                    end_imgColor();
                }
                else
                {
                    // 無いコマンドは、無視します。
                }
            }
            
            
            //var png = canvas.toDataURL();
            //document.getElementById( "img"+g_boardNumber ).src = png;

            g_boardNumber++;
        }


        /**
         * マスを塗りつぶす色
         */
        function colorMasu(
            value   // 例：'rgb(255,240,160)'
        )
        {
            ctxBG.fillStyle = value;
        }

        /**
         * 矢印を塗りつぶす色
         */
        function colorArrow(
            value   // 例：'rgb(255,240,160)'
        )
        {
            ctxARW.fillStyle = value;
            ctxARW.strokeStyle = value;
        }

        function drawMasu( masuHandle )
        {
            ctxBG.fillRect( masus_x_arr[masuHandle], masus_y_arr[masuHandle], masu_w, masu_h );
        }

        /**
         * マスの位置を初期設定します。
         */
        function makeBoard()
        {

            /* 2D描画 */
            ctxBG.beginPath();
            var x;
            var y;
            
            var masuHandle = 0;



            /* 将棋盤 */
            ctxBG.fillStyle = 'rgb(255,240,160)'; //塗りつぶしの色
            x = 10+110;
            y = 25;
            for( col=8; 0<=col; col-- )
            {
                for( row=0; row<9; row++ )
                {
                    masus_x_arr[masuHandle] = (masu_w+masu_spW)*col+x;
                    masus_y_arr[masuHandle] = (masu_h+masu_spH)*row+y;
                    
                    drawMasu( masuHandle );

                    ctxBG.strokeStyle = 'rgb( 220, 200, 80)';
                    ctxBG.strokeText( masuHandle, masus_x_arr[masuHandle], masus_y_arr[masuHandle]+10 )

                    masuHandle++;
                }
            }


            /* 先手駒台 */
            ctxBG.fillStyle = 'rgb(255,200,120)'; //塗りつぶしの色
            x = 10+110+255;
            for( col=3; 0<=col; col-- )
            {
                for( row=0; row<10; row++ )
                {
                    masus_x_arr[masuHandle] = (masu_w+masu_spW)*col+x;
                    masus_y_arr[masuHandle] = (masu_h+masu_spH)*row+y;
                
                    drawMasu( masuHandle );
                    ctxBG.strokeStyle = 'rgb( 220, 140, 60)';
                    ctxBG.strokeText( masuHandle, masus_x_arr[masuHandle], masus_y_arr[masuHandle]+10 )
                    masuHandle++;
                }
            }


            /* 後手駒台 */
            ctxBG.fillStyle = 'rgb(255,200,120)'; //塗りつぶしの色
            x = 10;
            y = 25;
            col;
            row;
            for( col=3; 0<=col; col-- )
            {
                for( row=0; row<10; row++ )
                {
                    masus_x_arr[masuHandle] = (masu_w+masu_spW)*col+x;
                    masus_y_arr[masuHandle] = (masu_h+masu_spH)*row+y;
                
                    drawMasu( masuHandle );
                    ctxBG.strokeStyle = 'rgb( 220, 140, 60)';
                    ctxBG.strokeText( masuHandle, masus_x_arr[masuHandle], masus_y_arr[masuHandle]+10 )
                    masuHandle++;
                }
            }
            
            /* 駒袋 */
            ctxBG.fillStyle = 'rgb(200,120,255)'; //塗りつぶしの色
            x = 10+110+255+110;
            for( col=3; 0<=col; col-- )
            {
                for( row=0; row<10; row++ )
                {
                    masus_x_arr[masuHandle] = (masu_w+masu_spW)*col+x;
                    masus_y_arr[masuHandle] = (masu_h+masu_spH)*row+y;
                
                    drawMasu( masuHandle );
                    ctxBG.strokeStyle = 'rgb( 110, 50, 150)';
                    ctxBG.strokeText( masuHandle, masus_x_arr[masuHandle], masus_y_arr[masuHandle]+10 )
                    masuHandle++;
                }
            }
        }

        /**
         * 文字
         */
        function drawText(
            text,
            x,
            y       // ベースライン
        )
        {
            /* 文字1 */
            
            ctxMAIN.beginPath();

            ctxMAIN.font = '400 20px/2 Unknown Font, sans-serif';
            ctxMAIN.strokeStyle = 'rgb( 10, 10, 10)';
            ctxMAIN.strokeText( text, x, y )
        }

        /**
         * 矢印
         *
         * 引用：http://k-hiura.cocolog-nifty.com/blog/2010/11/post-2a62.html
         */
        function drawArrow( fromMasu, toMasu ){

            // 矢印の頭のでかさ
            var w = 8;
            var h = 8;
            
            // 始点
            from_x = masus_x_arr[fromMasu] + masu_w/2;
            from_y = masus_y_arr[fromMasu] + masu_h/2;
            
            // 終点
            to_x   = masus_x_arr[toMasu  ] + masu_w/2;
            to_y   = masus_y_arr[toMasu  ] + masu_h/2;

            // 移動量
            var Vx= to_x-from_x;
            var Vy= to_y-from_y;
            // 対角線を走る長さ
            var v = Math.sqrt(Vx*Vx+Vy*Vy);
            // 単位
            var Ux= Vx/v; // 対角線上を動いた割合と同じだけの、xがずれる割合
            var Uy= Vy/v; // 対角線上を動いた割合と同じだけの、yがずれる割合


            // 矢印の線（始点～終点）
            ctxARW.beginPath();
            ctxARW.moveTo( from_x, from_y );
            ctxARW.lineTo( to_x  , to_y   );
            //ctxARW.strokeStyle = 'rgb( 10, 140, 10)';
            ctxARW.stroke();

            // 矢印のとんがり
            ctxARW.beginPath();
            // 左側
            ctxARW.moveTo( to_x - Uy*w - Ux*h, to_y + Ux*w - Uy*h ); // なんだかわからないがこれで動く
            ctxARW.lineTo( to_x, to_y );
            // 右側
            ctxARW.lineTo( to_x + Uy*w - Ux*h, to_y - Ux*w - Uy*h );
            // 塗り潰し
            ctxARW.closePath();                    // 始点と終点をつなげます
            //ctxARW.fillStyle = 'rgb(10,140,10)';   // 色
            ctxARW.fill();                         // 塗り
        }



        var g_srcRed_arr = new Array();
        var g_srcGreen_arr = new Array();
        var g_srcBlue_arr = new Array();
        var g_srcAlpha_arr = new Array();
        var g_dstRed_arr = new Array();
        var g_dstGreen_arr = new Array();
        var g_dstBlue_arr = new Array();
        var g_dstAlpha_arr = new Array();

        function begin_imgColor( colors_arr )
        {
            for ( var index=0; index<colors_arr.length; index++ )
            {
                g_srcRed_arr  [index] = colors_arr[index].sR;
                g_srcGreen_arr[index] = colors_arr[index].sG;
                g_srcBlue_arr [index] = colors_arr[index].sB;
                g_srcAlpha_arr[index] = colors_arr[index].sA;

                g_dstRed_arr  [index] = colors_arr[index].dR;
                g_dstGreen_arr[index] = colors_arr[index].dG;
                g_dstBlue_arr [index] = colors_arr[index].dB;
                g_dstAlpha_arr[index] = colors_arr[index].dA;
            }
        }

        function end_imgColor()
        {
            // クリアー
            g_srcRed_arr = new Array();
            g_srcGreen_arr = new Array();
            g_srcBlue_arr = new Array();
            g_srcAlpha_arr = new Array();
            g_dstRed_arr = new Array();
            g_dstGreen_arr = new Array();
            g_dstBlue_arr = new Array();
            g_dstAlpha_arr = new Array();
        }

        /**
         * 画像を表示します。
         */
        function drawImg( imgStr, masuHandle )
        {
            var img = new Image();
            // .htmlから見たパス
            img.src = "../Data/img/gkLog/"+imgStr+".png";
            // ↓ここから追加☆ｗｗ
            img.canvasName = 'board'+g_boardNumber;
            img.srcRed_arr   = new Array();
            img.srcGreen_arr = new Array();
            img.srcBlue_arr  = new Array();
            img.srcAlpha_arr = new Array();
            img.dstRed_arr   = new Array();
            img.dstGreen_arr = new Array();
            img.dstBlue_arr  = new Array();
            img.dstAlpha_arr = new Array();

            for( var i = 0; i<g_srcRed_arr.length; i++)
            {
                img.srcRed_arr  [i] = g_srcRed_arr  [i];
                img.srcGreen_arr[i] = g_srcGreen_arr[i];
                img.srcBlue_arr [i] = g_srcBlue_arr [i];
                img.srcAlpha_arr[i] = g_srcAlpha_arr[i];
                img.dstRed_arr  [i] = g_dstRed_arr  [i];
                img.dstGreen_arr[i] = g_dstGreen_arr[i];
                img.dstBlue_arr [i] = g_dstBlue_arr [i];
                img.dstAlpha_arr[i] = g_dstAlpha_arr[i];
            }
            // ↑ここまで追加☆ｗｗ

            img.onload = function()
            {
                var canvasIMG = document.getElementById(this.canvasName);
                if ( ! canvasIMG || ! canvasIMG.getContext ) { return false; }
                var ctxIMG = canvasIMG.getContext('2d');

                ctxIMG.drawImage( img, masus_x_arr[masuHandle], masus_y_arr[masuHandle] );
                
                
                //
                // 着色
                //

                if( navigator.userAgent.indexOf("Chrome")!=-1 )
                {
                    // グーグルクロームでは着色しません。ローカル環境でエラーが出たので。
                    
                    // アナウンス1
                    document.getElementById("announce1").style.visibility="visible";
                }
                else if(
                    0 < this.srcRed_arr.length
                )
                {
                    var input  = ctxIMG.getImageData( masus_x_arr[masuHandle], masus_y_arr[masuHandle], this.width, this.height );
                    var output = ctxIMG.createImageData( this.width, this.height );

                    for (var i = 0; i < this.width*this.height * 4; i += 4)
                    {
                        for( var iColors = 0; iColors<img.srcRed_arr.length; iColors++)
                        {
                            // 指定の色なら、変色します。
                            if(
                                input.data[i  ] == this.srcRed_arr  [iColors] &&
                                input.data[i+1] == this.srcGreen_arr[iColors] &&
                                input.data[i+2] == this.srcBlue_arr [iColors] &&
                                input.data[i+3] == this.srcAlpha_arr[iColors]
                            )
                            {
                                // 色変更
                                output.data[i    ] = this.dstRed_arr  [iColors]; // 赤
                                output.data[i + 1] = this.dstGreen_arr[iColors]; // 緑
                                output.data[i + 2] = this.dstBlue_arr [iColors]; // 青
                                output.data[i + 3] = this.dstAlpha_arr[iColors]; // アルファ（canvasの背景色と混じる？）
                            }
                            else
                            {
                                // コピー
                                output.data[i    ] = input.data[i    ]; // 赤
                                output.data[i + 1] = input.data[i + 1]; // 緑
                                output.data[i + 2] = input.data[i + 2]; // 青
                                output.data[i + 3] = input.data[i + 3]; // アルファ
                            }
                        }
                    }

                    ctxIMG.putImageData( output, masus_x_arr[masuHandle], masus_y_arr[masuHandle] );
                }
                else
                {
                    // 着色はありません。
                }
            };
        }


/**
 * アナウンス用のタグを用意しておきます。
 */
function createAnnounce1()
{
    var div = document.createElement("div");
    div.setAttribute( "id", "announce1" );
    div.setAttribute( "style",
        "visibility:hidden;"+
        "color:white;"+
        "background-color:gray;"+
        "border:solid 2px black;"+
        "padding:5px;"+
        "margin:5px;"
    );
    var text = document.createTextNode( "アナウンス：　別のブラウザに変えて見てくれ☆　このブラウザでは使えない機能があって、もっと色が付くかも☆ｗｗ");
    div.appendChild( text );
    document.body.appendChild( div );
}



