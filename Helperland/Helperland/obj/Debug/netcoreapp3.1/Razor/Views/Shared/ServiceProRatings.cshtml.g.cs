#pragma checksum "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\ServiceProRatings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d51bc50ee4acdb8642fca78500110696d94d8eee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_ServiceProRatings), @"mvc.1.0.view", @"/Views/Shared/ServiceProRatings.cshtml")]
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
#line 1 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d51bc50ee4acdb8642fca78500110696d94d8eee", @"/Views/Shared/ServiceProRatings.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_ServiceProRatings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div id=""SPRatings"" class=""allPageMain tableContents"" style=""display: none"">
    <div class=""customer-page-title-export"">
        <div class=""customer-page-title"">
            <h2>
                My Ratings
            </h2>
        </div>
        <div id=""export-btn"">
            <a href=""#"" role=""button"" id=""export"">Export</a>
        </div>
    </div>
    <table id=""SPRatingsTable"">
        <thead class=""d-none"">
            <tr>
                <th>
                    Service ID
                </th>
                <th>
                    Service Date
                </th>
                <th>
                    ratings
                </th>
                <th>
                    Comment
                </th>
            </tr>
        </thead>
        <tbody>

            <tr>
                <td>
                    <p>323436</p>
                    <p>Samay Nandaniya</p>
                </td>
                <td>
                    <div>
                   ");
            WriteLiteral("     <span><img src=\"img/upcomingService/calendar2.png\"");
            BeginWriteAttribute("alt", " alt=\"", 1201, "\"", 1207, 0);
            EndWriteAttribute();
            WriteLiteral("></span>\r\n                        <span class=\"upcoming-date\"><b>09/04/2018</b></span>\r\n                    </div>\r\n                    <div>\r\n                        <span><img src=\"img/upcomingService/timer.png\"");
            BeginWriteAttribute("alt", " alt=\"", 1421, "\"", 1427, 0);
            EndWriteAttribute();
            WriteLiteral(@"></span> <span class=""upcoming-time"">12:00-18:00</span>
                    </div>
                </td>
                <td>
                    <p><b>Ratings</b></p>

                    <div class=""d-flex"">
                        <div class=""spStars"">
                            <span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span>
                        </div>

                        <div><span class=""ms-2"">Very Good</span></div>
                    </div>
                </td>

                <td>
                    <p><b>Comments:</b></p>
                    <p></p>
                </td>

            </tr>

            <tr>
                <td>
                    <p>323436</p>
                    <p>Samay Nandaniya</p>
                </td>
                <td>
                    <div>
                        <span><img src=""img/upcomingService/calendar2.png""");
            BeginWriteAttribute("alt", " alt=\"", 2418, "\"", 2424, 0);
            EndWriteAttribute();
            WriteLiteral("></span>\r\n                        <span class=\"upcoming-date\"><b>09/04/2018</b></span>\r\n                    </div>\r\n                    <div>\r\n                        <span><img src=\"img/upcomingService/timer.png\"");
            BeginWriteAttribute("alt", " alt=\"", 2638, "\"", 2644, 0);
            EndWriteAttribute();
            WriteLiteral(@"></span> <span class=""upcoming-time"">12:00-18:00</span>
                    </div>
                </td>
                <td>
                    <p><b>Ratings</b></p>

                    <div class=""d-flex"">
                        <div class=""spStars"">
                            <span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span><span></span>
                        </div>

                        <div><span class=""ms-2"">Very Good</span></div>
                    </div>
                </td>

                <td>
                    <p><b>Comments:</b></p>
                    <p></p>
                </td>

            </tr>

        </tbody>
    </table>
</div>
");
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
