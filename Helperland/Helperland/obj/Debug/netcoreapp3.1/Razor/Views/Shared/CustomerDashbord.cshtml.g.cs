#pragma checksum "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fb29a20ddea7bf4dcf928a44fd859cabf11cb51f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_CustomerDashbord), @"mvc.1.0.view", @"/Views/Shared/CustomerDashbord.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb29a20ddea7bf4dcf928a44fd859cabf11cb51f", @"/Views/Shared/CustomerDashbord.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_CustomerDashbord : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Helperland.Models.CustomerDashbord>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "CustomerPage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "BookService", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("export"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<!-- dashbord starts here -->
<div id=""dashbord"" class=""allPageMain"">
    <div>
        <div class=""customer-page-title-export"" id=""dashbordTitle"">
            <div class=""customer-page-title"">
                <h2>
                    Current Service Requests
                </h2>
            </div>
            <div id=""export-btn"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fb29a20ddea7bf4dcf928a44fd859cabf11cb51f4842", async() => {
                WriteLiteral("Add new Service Request");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>



        <table id=""dashbordTable"">
            <thead>
                <tr>
                    <th>
                        Service Id
                    </th>
                    <th class=""dateTime"">
                        Service Details
                    </th>
                    <th>
                        Service Provider
                    </th>
                    <th class=""headingPayment"">
                        Payment
                    </th>

                    <th class=""text-center"">
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 45 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                 if (Model.Count() > 0)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                     foreach (var row in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr data-value=");
#nullable restore
#line 49 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                  Write(row.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"serviceRequestRow\">\r\n                            <td >\r\n                                ");
#nullable restore
#line 51 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                           Write(row.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td class=\"dateTime\">\r\n                                <div>\r\n                                    <span><img src=\"/img/upcomingService/calendar2.png\"");
            BeginWriteAttribute("alt", " alt=\"", 1878, "\"", 1884, 0);
            EndWriteAttribute();
            WriteLiteral("></span>\r\n                                    <span class=\"upcoming-date\"><b>");
#nullable restore
#line 56 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                              Write(row.ServiceStartDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></span>\r\n                                </div>\r\n                                <div>\r\n                                    <span><img src=\"/img/upcomingService/timer.png\"");
            BeginWriteAttribute("alt", " alt=\"", 2158, "\"", 2164, 0);
            EndWriteAttribute();
            WriteLiteral("></span>\r\n                                    <span class=\"upcoming-time\">");
#nullable restore
#line 60 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                           Write(row.StartTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 60 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                                            Write(row.EndTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                </div>\r\n                            </td>\r\n                            <td >\r\n");
#nullable restore
#line 64 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                 if (row.ServiceProvider != null) { 

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"customer-sh-SP\">\r\n                                    <div>\r\n                                        <span class=\"cap-span\"><img src=\"/img/customer-serviceHistory/cap.png\" class=\"cap\"");
            BeginWriteAttribute("alt", " alt=\"", 2684, "\"", 2690, 0);
            EndWriteAttribute();
            WriteLiteral("></span>\r\n                                    </div>\r\n                                    <div class=\"sp-detail\">\r\n                                        <p class=\"SP-name\">");
#nullable restore
#line 70 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                      Write(row.ServiceProvider);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                        <div class=\"spStars\">\r\n");
#nullable restore
#line 72 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                             for (int i = 0; i < Math.Ceiling(row.SPRatings * 2); i++)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("<span class=\"stars yellowStars\"></span>");
#nullable restore
#line 73 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                             for (int i = (int)Math.Ceiling(row.SPRatings * 2); i < 10; i++)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("<span class=\"stars\"></span>");
#nullable restore
#line 75 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </div>\r\n                                        <span class=\"SP-stars\">");
#nullable restore
#line 77 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                          Write(row.SPRatings);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 80 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                \r\n                            </td>\r\n                            <td >\r\n                                <div class=\"allPagePayment\">\r\n\r\n                                    <span class=\"payment\">");
#nullable restore
#line 86 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                                                     Write(row.TotalCost);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>
                                    &euro;
                                </div>
                            </td>

                            <td class=""allPageActionButtons dashbordActions"">
                                <button type=""button"" id=""customerDashbordRescheduleBtn"" data-bs-toggle=""modal"" data-bs-target=""#serviceRequestReschedule""");
            BeginWriteAttribute("value", " value=", 4186, "", 4214, 1);
#nullable restore
#line 92 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
WriteAttributeValue("", 4193, row.ServiceRequestId, 4193, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"customerReschedule\">Reschedule</button>\r\n                                <button type=\"button\" id=\"customerDashbordCancelBtn\" data-bs-toggle=\"modal\" data-bs-target=\"#serviceRequestCancelModal\"");
            BeginWriteAttribute("value", " value=", 4414, "", 4442, 1);
#nullable restore
#line 93 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
WriteAttributeValue("", 4421, row.ServiceRequestId, 4421, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"customerCancel\">Cancel</button>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 96 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"

                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerDashbord.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n                <!-- 1st row -->\r\n                <!-- 1 row ends -->\r\n            </tbody>\r\n        </table>\r\n\r\n    </div>\r\n\r\n</div>\r\n<!-- dashbord ends here -->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Helperland.Models.CustomerDashbord>> Html { get; private set; }
    }
}
#pragma warning restore 1591
