#pragma checksum "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\Shared\CustomerMySetting.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9684d46b0c19cacd91eea8581ca152a27f941657"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_CustomerMySetting), @"mvc.1.0.view", @"/Views/Shared/CustomerMySetting.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9684d46b0c19cacd91eea8581ca152a27f941657", @"/Views/Shared/CustomerMySetting.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_CustomerMySetting : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<!-- my setting page starts -->
<div id=""mysetting"" class=""allPageMain "">

    <ul class=""mysettingUl"">
        <li class=""mysettingTab active"" id=""myDetailsTab"" onclick=""mydetails()"">
            <span class=""mysettingTabImg"">
                <svg xmlns=""http://www.w3.org/2000/svg"" class=""h-6 w-6"" fill=""none"" viewBox=""0 0 24 24"" stroke=""currentColor"">
                    <path stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""1"" d=""M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"" />
                </svg>

            </span>
            <span class=""mysettingTabText""> My Details </span>
        </li>
        <li class=""mysettingTab"" id=""myAddressTab"" onclick=""myaddress()"">
            <span class=""mysettingTabImg"">
                <svg xmlns=""http://www.w3.org/2000/svg"" class=""h-6 w-6"" fill=""none"" viewBox=""0 0 24 24"" stroke=""currentColor"">
                    <path stroke-linecap=""round"" stroke-linejoin=""roun");
            WriteLiteral(@"d"" stroke-width=""1"" d=""M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"" />
                    <path stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""1"" d=""M15 11a3 3 0 11-6 0 3 3 0 016 0z"" />
                </svg>
            </span>
            <span class=""mysettingTabText""> My Address </span>
        </li>
        <li class=""mysettingTab"" id=""changePasswordTab"" onclick=""changepassword()"">
            <span class=""mysettingTabImg key"">
                <svg xmlns=""http://www.w3.org/2000/svg"" className=""h-6 w-6"" fill=""none"" viewBox=""0 0 24 24"" stroke=""currentColor"">
                    <path strokeLinecap=""round"" strokeLinejoin=""round"" strokeWidth={1} d=""M15 7a2 2 0 012 2m4 0a6 6 0 01-7.743 5.743L11 17H9v2H7v2H4a1 1 0 01-1-1v-2.586a1 1 0 01.293-.707l5.964-5.964A6 6 0 1121 9z"" />
                </svg>
            </span>
            <span class=""mysettingTabText""> Change Password </span>
        </li>
    </ul>

    <!-- my details div starts --");
            WriteLiteral(">\r\n    <div class=\"mysettingDiv\" id=\"myDetailsMySetting\">\r\n\r\n        <p class=\"mySettingTitle\"><b>My Details</b></p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9684d46b0c19cacd91eea8581ca152a27f9416575963", async() => {
                WriteLiteral(@"
            <div class=""container mt-3"">
                <div class=""row"">
                    <div class=""col-lg-4"">
                        <label for=""mySettingFname""> First Name </label>
                        <input type=""text"" class=""form-control"" placeholder=""First Name"" id=""mySettingFname"" />
                    </div>
                    <div class=""col-lg-4"">
                        <label for=""mySettingLname""> Last Name </label>
                        <input type=""text"" placeholder=""Last Name"" class=""form-control"" id=""mySettingLname"" />
                    </div>
                    <div class=""col-lg-4"">
                        <label for=""mySettingEmail""> E-mail Address </label>
                        <input type=""email"" class=""form-control"" placeholder=""E-mail"" id=""mySettingEmail"" disabled />
                    </div>
                </div>
                <div class=""row cover"">
                    <div class=""col-lg-4"">
                        <label for=""mySettingMobile");
                WriteLiteral(@""">Mobile Number</label>
                        <div class=""input-group"" class=""mobile-box"">
                            <span class=""input-group-text"">+49</span>
                            <input type=""tel"" class=""form-control"" placeholder=""Mobile Number"" aria-label=""mobile"" aria-describedby=""basic-addon1"" id=""mySettingMobile"" />
                        </div>
                    </div>
                    <div class=""col-lg-4"">
                        <label for=""mySettingDob""> Date of Birth </label>
                        <input type=""date"" class=""form-control"" name=""birthdate"" id=""mySettingDob"" />
                    </div>
                </div>
                <button type=""button"" id=""myDetailsSave"" class=""mySettingSave"">Save</button>
            </div>
        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>
    <!-- my details div ends -->
    <!-- my address div starts -->
    <div class=""mysettingDiv d-none"" id=""myAddressMySetting"">
        <p class=""mySettingTitle""><b>My Address</b></p>
        <table>
            <thead>
                <tr>
                    <th>Addresses</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <p><strong>Address: </strong>Maruti park 20B, 364002 Bhavnagar</p>
                        <p><strong>Phone number: </strong>8200035128</p>
                    </td>

                    <td class=""myAddressBtns"">
                        <button class=""myAddressButton"">
                            <svg xmlns=""http://www.w3.org/2000/svg""
                                 class=""edit-icon""
                                 fill=""none""
                                 viewBox=""0 0 24 24""
                                 stroke=""curren");
            WriteLiteral(@"tColor"">
                                <path stroke-linecap=""round""
                                      stroke-linejoin=""round""
                                      stroke-width=""2""
                                      d=""M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z"" />
                            </svg>
                        </button>
                        <button class=""myAddressButton"">
                            <svg xmlns=""http://www.w3.org/2000/svg""
                                 class=""delete-icon""
                                 fill=""none""
                                 viewBox=""0 0 24 24""
                                 stroke=""currentColor"">
                                <path stroke-linecap=""round""
                                      stroke-linejoin=""round""
                                      stroke-width=""2""
                                      d=""M19 7l-.867 12.142A2 2 0 0116.138 21H");
            WriteLiteral(@"7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"" />
                            </svg>
                        </button>
                    </td>

                </tr>

                <tr>
                    <td>
                        <p><strong>Address: </strong>Maruti park 20B, 364002 Bhavnagar</p>
                        <p><strong>Phone number: </strong>8200035128</p>
                    </td>

                    <td class=""myAddressBtns"">
                        <button class=""myAddressButton"">
                            <svg xmlns=""http://www.w3.org/2000/svg""
                                 class=""edit-icon""
                                 fill=""none""
                                 viewBox=""0 0 24 24""
                                 stroke=""currentColor"">
                                <path stroke-linecap=""round""
                                      stroke-linejoin=""round""
                                      stroke-width=""2""");
            WriteLiteral(@"
                                      d=""M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z"" />
                            </svg>
                        </button>
                        <button class=""myAddressButton"">
                            <svg xmlns=""http://www.w3.org/2000/svg""
                                 class=""delete-icon""
                                 fill=""none""
                                 viewBox=""0 0 24 24""
                                 stroke=""currentColor"">
                                <path stroke-linecap=""round""
                                      stroke-linejoin=""round""
                                      stroke-width=""2""
                                      d=""M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"" />
                            </svg>
                        </button>
                    </td>");
            WriteLiteral(@"

                </tr>
            </tbody>
        </table>


        <div>
            <button type=""button"" class=""mySettingSave"" id=""mySettingAddNewAddress"" data-bs-toggle=""modal"" data-bs-target=""#mySettingAddAddressModal"">Add New Address</button>
        </div>
    </div>
    <!-- my address div ends -->
    <!-- change password starts -->
    <div class=""mysettingDiv d-none"" id=""changePasswordMySetting"">
        <p class=""mySettingTitle""><b>Change Password</b></p>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9684d46b0c19cacd91eea8581ca152a27f94165713953", async() => {
                WriteLiteral(@"
            <div>
                <p>Old Password</p>
                <input type=""text"" placeholder=""Current Password"">
            </div>
            <div>
                <p>new Password</p>
                <input type=""text"" placeholder=""New Password"">
            </div>
            <div>
                <p>Confirm Password</p>
                <input type=""text"" placeholder=""Confirm Password"">
            </div>

            <div>
                <button type=""button"" class=""mySettingSave"">Save</button>
            </div>
        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n    <!-- change password ends -->\r\n\r\n</div>\r\n<!-- my setting page ends -->\r\n");
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
