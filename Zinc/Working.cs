using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Zinc
{
    public class Working
    {
        public Working()
        {

        }

        public void Test1()
        {
            Stream s = File.Open("C:\\Temp\\src.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(s);

            CSharpCodeProvider cscProvider = new CSharpCodeProvider();
            ICodeGenerator cscg = cscProvider.CreateGenerator(sw);
            CodeGeneratorOptions cop = new CodeGeneratorOptions();

            CodeSnippetCompileUnit csu1 = new CodeSnippetCompileUnit("using System");
            CodeSnippetCompileUnit csu2 = new CodeSnippetCompileUnit("using System.IO");
            cscg.GenerateCodeFromCompileUnit(csu1, sw, cop);
            cscg.GenerateCodeFromCompileUnit(csu2, sw, cop);

            sw.WriteLine();

            CodeNamespace cnsCodeDom = new CodeNamespace("CodeDom");

            CodeTypeDeclaration ctd = new CodeTypeDeclaration();
            ctd.IsClass = true;
            ctd.Name = "Briefcase";
            ctd.TypeAttributes = System.Reflection.TypeAttributes.Public;

            sw.WriteLine();

            CodeMemberField cmfBriefcaseName = new CodeMemberField("string", "m_BriefcaseName");
            cmfBriefcaseName.Attributes = MemberAttributes.Private;

            


        }
    }
}
