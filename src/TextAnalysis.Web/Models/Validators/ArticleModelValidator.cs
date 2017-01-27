
using FluentValidation;

namespace TextAnalysis.Web.Models.Validators
{
    public class ArticleModelValidator : AbstractValidator<ArticleModel>
    {
        public ArticleModelValidator()
        {
            RuleFor(model => model.Url).NotNull();
        }
    }
}