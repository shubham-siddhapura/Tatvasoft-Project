#pragma checksum "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "61ca35ab19130d20364a7cc1613a63fcf443b515"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServicePro_SPUpcomingService), @"mvc.1.0.view", @"/Views/ServicePro/SPUpcomingService.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"61ca35ab19130d20364a7cc1613a63fcf443b515", @"/Views/ServicePro/SPUpcomingService.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5f94cf04a7ec23f27ac33992ef127038e0b3154", @"/Views/_ViewImports.cshtml")]
    public class Views_ServicePro_SPUpcomingService : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
  
    ViewData["Title"] = "SPUpcomingService";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61ca35ab19130d20364a7cc1613a63fcf443b5153597", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Helperland | Upcoming Service</title>

    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3"" crossorigin=""anonymous"">


    <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
    <link href=""https://fonts.googleapis.com/css2?family=Roboto:wght@500&display=swap"" rel=""stylesheet"">

    <script src=""https://kit.fontawesome.com/5602f8a8c9.js"" crossorigin=""anonymous""></script>

    <link rel=""stylesheet""
          type=""text/css""
          href=""https://cdn.datatables.net/1.11.3/css/jquery.dataTables.css"" />

    <script type=""text/javascript""
            charset=""utf8""
            src=""https://cdn.datatables.n");
                WriteLiteral("et/1.11.3/js/jquery.dataTables.js\"></script>\r\n\r\n    <link rel=\"stylesheet\" href=\"https://unpkg.com/leaflet@1.7.1/dist/leaflet.css\"\r\n          integrity=\"sha512-xodZBNTC5n17Xt2atTPuE1HxjVMSvLVW9ocqUKLsCC5CXdbqCmblAshOMAS6/keqq/sMZMZ19scR4PsZChSR7A==\"");
                BeginWriteAttribute("crossorigin", "\r\n          crossorigin=\"", 1371, "\"", 1396, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n\r\n    <link rel=\"stylesheet\" href=\"https://cdn.jsdelivr.net/npm/fullcalendar@5.10.2/main.min.css\">\r\n\r\n    <link rel=\"stylesheet\" href=\"/css/style.css\">\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61ca35ab19130d20364a7cc1613a63fcf443b5156285", async() => {
                WriteLiteral("\r\n\r\n    <!-- nav starts -->\r\n    ");
#nullable restore
#line 43 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
Write(await Html.PartialAsync("FaqNav"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n    <!-- nav ends -->\r\n    <!-- main starts -->\r\n    <main>\r\n        <!-- header starts -->\r\n        <section id=\"upcoming-header\">\r\n            <h1>\r\n                <span>Welcome,</span> ");
#nullable restore
#line 50 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
                                 Write(TempData["name"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
            </h1>
        </section>
        <!-- header ends -->
        <!-- main section starts -->
        <section>
            <div id=""upcoming-main"">

                <div id=""upcoming-desktop-flex"">
                    <div id=""upcoming-menu-desktop"">
                        <ul>
                            <li id=""spDashbordTab"" class=""spTabs""> Dashbord</li>
                            <li id=""spNewServiceRequestTab"" class=""active spTabs"">New Service Requests</li>
                            <li id=""spUpcomingServiceTab"" class=""spTabs"">Upcoming Service</li>
                            <li id=""spServiceScheduleTab"" class=""spTabs"">Service Schedule</li>
                            <li id=""spServiceHistoryTab"" class=""spTabs"">Service History</li>
                            <li id=""spMyRatingsTab"" class=""spTabs"">My Ratings</li>
                            <li id=""spBlockCustomerTab"" class=""spTabs"">Block Customer</li>
                        </ul>

                    </div>

      ");
                WriteLiteral("              ");
#nullable restore
#line 72 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
               Write(await Html.PartialAsync("ServiceProUpcomingService"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                    ");
#nullable restore
#line 74 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
               Write(await Html.PartialAsync("ServiceProMySetting"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                    ");
#nullable restore
#line 76 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
               Write(await Html.PartialAsync("ServiceProRatings"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                    ");
#nullable restore
#line 78 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
               Write(await Html.PartialAsync("ServiceProNewServiceRequest"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                    ");
#nullable restore
#line 80 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
               Write(await Html.PartialAsync("ServiceProServiceHistory"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                    ");
#nullable restore
#line 82 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
               Write(await Html.PartialAsync("ServiceProBlockCustomer"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                    ");
#nullable restore
#line 84 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
               Write(await Html.PartialAsync("ServiceSchedule"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n\r\n                </div>\r\n            </div>\r\n        </section>\r\n        <!-- main section ends -->\r\n\r\n    </main>\r\n    <!-- main ends -->\r\n    <!-- footer starts -->\r\n    ");
#nullable restore
#line 95 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
Write(await Html.PartialAsync("Footer"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n    <!-- footer ends -->\r\n\r\n    ");
#nullable restore
#line 98 "D:\Tatvasoft\Tatvasoft Project\Helperland\Helperland\Views\ServicePro\SPUpcomingService.cshtml"
Write(await Html.PartialAsync("ServiceProModals"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"

    <script>
        function dashbord() {
            document.getElementById(""dashbord"").style.display = ""block"";
            document.getElementById(""upcoming-table"").style.display = ""none"";
        }
        function upcoming() {
            document.getElementById(""dashbord"").style.display = ""none"";
            document.getElementById(""upcoming-table"").style.display = ""block"";
        }
    </script>

    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"" integrity=""sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p"" crossorigin=""anonymous""></script>


    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js""></script>
    <script type=""text/javascript""
            src=""https://cdn.datatables.net/v/dt/dt-1.11.3/r-2.2.9/rg-1.1.4/datatables.min.js""></script>
    <script src=""https://cdn.datatables.net/responsive/2.2.9/js/dataTables.responsive.min.js""></script>
    <script src=""https://cdn.datatables.");
                WriteLiteral(@"net/buttons/2.1.0/js/dataTables.buttons.min.js""></script>
    <script src=""https://cdn.datatables.net/buttons/2.1.0/js/buttons.html5.min.js""></script>

    <script src=""https://cdn.jsdelivr.net/npm/fullcalendar@5.10.2/main.min.js""></script>


    <!-- map js-->
    <script src=""https://unpkg.com/leaflet@1.7.1/dist/leaflet.js""
            integrity=""sha512-XQoYMqMTK8LvdxXYG3nZ448hOEQiglfqkJs1NOQV44cWnUrBc8PkAOcXy20w0vlaXaVUearIOBhiXZ5V3ynxwA==""");
                BeginWriteAttribute("crossorigin", "\r\n            crossorigin=\"", 5143, "\"", 5170, 0);
                EndWriteAttribute();
                WriteLiteral(@"></script>



    <!--My Add map js-->
    <script src=""/js/addMap.js""></script>

    <script src=""/js/CustomDate.min.js""></script>

    <!-- tabination js-->
    <script src=""/js/SPTabination.js""></script>

    <!--Service History js-->
    <script src=""/js/ServiceHistoryAjax.js""></script>

    <!-- New Service Request js-->
    <script src=""/js/NewServiceRequestsAjax.js""></script>

    <!--upcoming js-->
    <script src=""/js/SPUpcomingServices.js""></script>

    <!--SP ratings js-->
    <script src=""/js/SPRatings.js""></script>

    <!--My setting js-->
    <script src=""/js/SPMySetting.js""></script>

    <!--My Modals js-->
    <script src=""/js/SPModals.js""></script>

    <!--SP Block Customer js-->
    <script src=""/js/SPBlockCustomer.js""></script>

    <script src=""/js/SPScheduleService.js""></script>

");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
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
