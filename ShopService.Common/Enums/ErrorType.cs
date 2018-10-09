using System.ComponentModel;

namespace ShopService.Common.Enums
{
    public enum ErrorType
    {
        [Description("Unknown description")]
        UnknownError = 0,

        [Description("Invalid request model")]
        InvalidRequestModel = 2,

        [Description("Invalid operation")]
        InvalidOperation = 4,

        #region shop

        [Description("Goods does not exist")]
        GoodsDoesNotExist = 100,

        [Description("Goods category does not exist")]
        GoodsCategoryDoesNotExist = 105,

        [Description("Category does not exist")]
        CategoryDoesNotExist = 110,

        [Description("Basket does not exist")]
        BasketDoesNotExist = 115,

        [Description("Basket item count less than need")]
        BasketItemCountLessThenNeed = 120,

        [Description("Duplicated email or user name")]
        DuplicatedEmailOrUserName = 125,

        [Description("Category With This Name Already Exist")]
        CategoryWithThisNameAlreadyExist = 130

        #endregion
    }
}