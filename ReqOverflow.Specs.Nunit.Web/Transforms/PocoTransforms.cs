using Reqnroll;

namespace ReqOverflow.Specs.Nunit.Web.Steps;

[Binding]
public static class PocoTransforms
{
    [StepArgumentTransformation]

    public static IEnumerable<QuestionData> TransformQuestionData(DataTable table) => table.CreateSet<QuestionData>();

}
