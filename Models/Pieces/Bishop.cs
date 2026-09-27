//هنا انا بقوله خليني استخدم كل الclasses ال موجوده في ENums 
using Chess_Game.Enums;
// تحديد الـ namespace الذي ينتمي إليه كلاس Bishop
namespace Chess_Game.Models.Pieces;

// تعريف كلاس Bishop وجعله يرث من الكلاس الأساسي Piece
public class Bishop : Piece
{
    // تحديد الرمز الخاص بقطعة الـ Bishop وهو الحرف B 
    public override char Symbol => 'B';

    // إنشاء Constructor لاستقبال لون القطعة ومكانها على لوحة الشطرنج
    public Bishop(PieceColor color, Position position)
        // إرسال اللون والمكان إلى Constructor الموجود في الكلاس الأب Piece
        : base(color, position)
    {
    }

    // تحديد جميع الحركات الصحيحة التي يمكن أن تتحركها قطعة الـ Bishop
    public override List<Position> GetValidMoves(Board board)
    {
        // تحديد الاتجاهات الأربعة التي يتحرك فيها الـ Bishop بشكل قطري
        var directions = new List<(int Row, int Column)>
        {
            // الاتجاه القطري لأعلى ولليسار
            (-1, -1),

            // الاتجاه القطري لأعلى ولليمين
            (-1, 1),

            // الاتجاه القطري لأسفل ولليسار
            (1, -1),

            // الاتجاه القطري لأسفل ولليمين
            (1, 1)
        };

        // استخدام الدالة المساعدة الموجودة في Piece لحساب جميع الحركات
        // مع الاستمرار في الاتجاه حتى الوصول إلى نهاية اللوحة أو قطعة أخرى
        return GetSlidingMoves(board, directions);
    }
}