namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_NodeAssert361
    {


        public static void AssertNariMove(Node<IMove, KyokumenWrapper> hubNode, string hint)
        {
            /*
            hubNode.Foreach_NextNodes((string key, Node<ShootingStarlightable, KyokumenWrapper> nextNode, ref bool toBreak) =>
            {
                ShootingStarlightable move = nextNode.Key;
                Starlightable lightable = move.Now;
                RO_Star_Koma koma = Util_Starlightable.AsKoma(lightable);

                if (KomaSyurui14Array.IsNari(koma.Syurui))
                {
                    MessageBox.Show("指し手に成りが含まれています。[" + koma.Masu.Word + "][" + koma.Syurui + "]", "デバッグ:" + hint);
                    //System.Console.WriteLine("指し手に成りが含まれています。");
                    toBreak = true;
                    goto gt_EndMethod;
                }
            gt_EndMethod:
                ;
            });
             */
        }

    }
}
