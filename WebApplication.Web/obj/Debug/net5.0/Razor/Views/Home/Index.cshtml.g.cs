#pragma checksum "D:\BTEC\ad\win\Win-Ok\WebApplication.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b224794447c8fc99644a0544ff6b66cef836461b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\BTEC\ad\win\Win-Ok\WebApplication.Web\Views\_ViewImports.cshtml"
using WebApplication.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\BTEC\ad\win\Win-Ok\WebApplication.Web\Views\_ViewImports.cshtml"
using WebApplication.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b224794447c8fc99644a0544ff6b66cef836461b", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55dcb12be480a17ed0836b14daa9689fbfa158f6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\BTEC\ad\win\Win-Ok\WebApplication.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
    Layout = "~/Views/Shared/_Layout_trainee.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        let html = """";
        const urlImg = ""https://localhost:5001/store-image/"";
        console.log(user.id)
        $.ajax({
            url: ""https://localhost:5001/api/Users/GetCourseByUserId?Id=""+user.id,
            type: ""GET"",
            success: function (response) {
                console.log(response)
                for (let i of response.resultObj.courses) {
                    html += `<tr>
                          <td>${i.name}</td>
                          <td>${i.description}</td>
                          <td><img src=${urlImg+i.image} style=""height:50px;width:100px"" /></td>
                          <td>${moment(i.start_Date).format(""DD/MM/YYYY"")}</td>
                          <td>${moment(i.end_Date).format(""DD/MM/YYYY"")}</td>
                          <td>${moment(i.createTime).format(""DD/MM/YYYY"")}</td>
                          <td><label class=""badge badge-danger"">Doing</label></td>
                        </tr>`
                }
           ");
                WriteLiteral("     console.log(moment().format())\r\n                $(\"#content\").html(html);\r\n            }\r\n        })\r\n    </script>\r\n");
            }
            );
            WriteLiteral(@"
<section>
    <div class=""row"">
      <div class=""col-lg-12 grid-margin stretch-card"">
              <div class=""card"">
                <div class=""card-body"">
                  <h4 class=""card-title"">Courses Table</h4>
                  <p class=""card-description"">
                    Add Course <code>.Create</code>
                  </p>
                  <div class=""table-responsive"">
                    <table class=""table table-hover"">
                      <thead>
                        <tr>
                          <th>Course Name</th>
                          <th>Course Description</th>
                          <th>Course Image/th>
                          <th>Start Date</th>
                          <th>End Date</th>
                          <th>Creation Time</th>
                          <th>Status</th>
                        </tr>
                      </thead>
                      <tbody id=""content"">
                      </tbody>
                    </table>
");
            WriteLiteral("                  </div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n    </div>\r\n</section>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
