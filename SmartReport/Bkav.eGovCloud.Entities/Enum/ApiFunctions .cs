using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// 47 tên hàm API
    /// </summary>
    public enum ApiFunctions
    {
        #region"Office"

        /// <summary>
        /// Lấy ra 1 list cơ quan cha
        /// </summary>
        [Description("IEnumerable<Office> GetParentOffices(int parentId)")]
        GetParentOffices = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<Office> GetAllOffice()")]
        GetAllOffice = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("Office GetOfficeByDoctypeId(Guid doctypeId)")]
        GetOfficeByDoctypeId = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<Office> GetAllParentOffice()")]
        GetAllParentOffice = 4,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<Office> GetOfficesByLevelId(int levelId)")]
        GetOfficesByLevelId = 5,

        /// <summary>
        /// 
        /// </summary>
        [Description("Office GetOfficeById(int officeId)")]
        GetOfficeById = 6,

        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<Office>> GetOffices()")]
        GetOffices = 7,

        #endregion

        #region "DocField"

        /// <summary>
        /// 
        /// </summary>
        [Description("DocField GetDocFieldByDoctypeId(Guid doctypeId)")]
        GetDocFieldByDoctypeId = 8,

        /// <summary>
        /// 
        /// </summary>
        [Description(" IEnumerable<DocField> GetAllDocField()")]
        GetAllDocField = 9,

        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<DocField>> GetDocFields()")]
        GetDocFields = 10,

        /// <summary>
        /// 
        /// </summary>
        [Description("DocField GetDocFieldById(int id)")]
        GetDocFieldById = 11,

        #endregion

        #region "Law"

        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<Law>> GetLaws()")]
        GetLaws = 12,

        /// <summary>
        /// 
        /// </summary>
        [Description("Law GetLawById(int id)")]
        GetLawById = 13,

        /// <summary>
        /// 
        /// </summary>
        [Description("string GetNumberSign(int lawId)")]
        GetNumberSign = 43,

        #endregion

        #region "Level"

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<Level> GetLevelsByType(int type)")]
        GetLevelsByType = 14,

        /// <summary>
        /// 
        /// </summary>
        [Description("int GetLevelByType(int type)")]
        GetLevelByType = 15,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<Level> GetAllLevel()")]
        GetAllLevel = 16,

        /// <summary>
        /// 
        /// </summary>
        [Description("Level GetLevelByDoctypeId(Guid doctypeId)")]
        GetLevelByDoctypeId = 17,

        /// <summary>
        /// 
        /// </summary>
        [Description("Level GetLevelByOfficeId(int officeId)")]
        GetLevelByOfficeId = 18,

        /// <summary>
        /// 
        /// </summary>
        [Description("Level GetLevelById(int id)")]
        GetLevelById = 19,

        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<Level>> GetLevels")]
        GetLevels = 20,

        #endregion

        #region "DocType"

        /// <summary>
        /// 
        /// </summary>
        [Description("IList<DocType> GetDocTypesByDocFieldId(int docfieldId)")]
        GetDocTypesByDocFieldId = 21,

        /// <summary>
        /// 
        /// </summary>
        [Description(" Dictionary<int, List<DocType>> GetDocTypes")]
        GetDocTypes = 22,

        /// <summary>
        /// 
        /// </summary>
        [Description("DocType GetDocTypeById(Guid id)")]
        GetDocTypeById = 23,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetDocTypesByActionLevelWithDoctypeName(string doctypeName)")]
        GetDocTypesByActionLevelWithDoctypeName = 24,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetDocTypesByDoctypeName(string doctypeName)")]
        GetDocTypesByDoctypeName = 25,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetDocTypesByNameNotMarks(string nameNotMarks)")]
        GetDocTypesByNameNotMarks = 26,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetDocTypesByActionLevelWithDoctypeNameAndOfficeId(string doctypeName, int officeId)")]
        GetDocTypesByActionLevelWithDoctypeNameAndOfficeId = 27,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetDocTypesByDoctypeNameAndOfficeId(string doctypeName, int officeId)")]
        GetDocTypesByDoctypeNameAndOfficeId = 28,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetAllDocType()")]
        GetAllDocType = 29,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetMostViewedDocTypes()")]
        GetMostViewedDocTypes = 30,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<DocType> GetDocTypesByActionLevel()")]
        GetDocTypesByActionLevel = 31,

        #endregion

        #region "Document"

        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<Document>> GetDocuments")]
        GetDocuments = 45,

        /// <summary>
        /// 
        /// </summary>
        [Description("Document GetDocumentByDoccode(string doccode)")]
        GetDocumentByDoccode = 46,

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<Document> GetAllDocument()")]
        GetAllDocument = 47,

        #endregion

        #region "Form"
        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<Form>> GetForms(")]
        GetForms = 32,

        /// <summary>
        /// 
        /// </summary>
        [Description("string GetJson(Guid formId)")]
        GetJson = 33,

        /// <summary>
        /// 
        /// </summary>
        [Description("Form GetFormById(Guid formId)")]
        GetFormById = 34,

        #endregion

        #region "FormGroup"

        /// <summary>
        /// 
        /// </summary>
        [Description("IEnumerable<FormGroup> GetAllFormGroup()")]
        GetAllFormGroup = 35,

        /// <summary>
        /// 
        /// </summary>
        [Description("Form GetForm(Guid formId)")]
        GetForm = 36,

        #endregion

        #region "Guide"

        /// <summary>
        /// 
        /// </summary>
    [Description(" IEnumerable<Guide> GetAllGuide()")]
        GetAllGuide = 37,

        /// <summary>
        /// 
        /// </summary>
       [Description("Dictionary<int, List<Guide>> GetGuides()")]
        GetGuides = 38,

        /// <summary>
        /// 
        /// </summary>
        [Description("Guide GetGuideByGuideUrl(string guideUrl)")]
        GetGuideByGuideUrl = 39,

        #endregion

        #region"Question"

        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<Question>> GetQuestions")]
        GetQuestions = 40,

        /// <summary>
        /// 
        /// </summary>
        [Description("Dictionary<int, List<Question>> GetQuestionsForHome()")]
        GetQuestionsForHome = 41,

        /// <summary>
        /// 
        /// </summary>
        [Description("Question GetQuestionByTagName(string tag)")]
        GetQuestionByTagName = 42,

        /// <summary>
        /// 
        /// </summary>
        [Description("Question GetQuestionById(int id)")]
        GetQuestionById = 44

        #endregion

        #region "File"
        #endregion

    }
}
