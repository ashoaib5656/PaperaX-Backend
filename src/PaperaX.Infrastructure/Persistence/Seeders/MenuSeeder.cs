using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaperaX.Domain.Entities;
using PaperaX.Domain.Enums;

namespace PaperaX.Infrastructure.Persistence.Seeders
{
    public static class MenuSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Seed Roles
            var adminRole = await EnsureRoleAsync(context, "Admin", "ADMIN", "Administrator Role");
            var customerRole = await EnsureRoleAsync(context, "Customer", "CUSTOMER", "Customer Role");
            var guestRole = await EnsureRoleAsync(context, "Guest", "GUEST", "Guest Role");

            await context.SaveChangesAsync();

            // Seed Footer Menus
            await SeedFooterMenusAsync(context, guestRole, adminRole, customerRole);

            // Seed TopNavbar Menus (Public)
            await SeedNavbarMenusAsync(context, guestRole, adminRole, customerRole);

            // Seed Admin Sidebar Menus
            await SeedAdminSidebarMenusAsync(context, adminRole);

            // Seed Customer Sidebar Menus
            await SeedCustomerSidebarMenusAsync(context, customerRole);
            
            await context.SaveChangesAsync();
        }

        private static async Task<Role> EnsureRoleAsync(ApplicationDbContext context, string name, string code, string description)
        {
            var role = await context.Roles.FirstOrDefaultAsync(r => r.Code == code);
            if (role == null)
            {
                role = new Role { Name = name, Code = code, Description = description, IsActive = true };
                context.Roles.Add(role);
            }
            return role;
        }

        private static async Task SeedFooterMenusAsync(ApplicationDbContext context, params Role[] roles)
        {
            if (await context.Menus.AnyAsync(m => m.Placement == MenuPlacement.Footer)) goto AssignRoles;

            var privacy = new Menu { Title = "Privacy Policy", Code = "PRIVACY", Route = "/privacy", Placement = MenuPlacement.Footer, OrderNo = 1 };
            var terms = new Menu { Title = "Terms of Service", Code = "TERMS", Route = "/terms", Placement = MenuPlacement.Footer, OrderNo = 2 };

            context.Menus.AddRange(privacy, terms);
            await context.SaveChangesAsync();

            await context.SaveChangesAsync();

        AssignRoles:
            var footerMenus = await context.Menus.Where(m => m.Placement == MenuPlacement.Footer).ToListAsync();
            foreach (var m in footerMenus)
            {
                AssignRolesToMenu(context, m, roles);
            }
            await context.SaveChangesAsync();
        }

        private static async Task SeedNavbarMenusAsync(ApplicationDbContext context, Role guestRole, Role adminRole, Role customerRole)
        {
            if (await context.Menus.AnyAsync(m => m.Placement == MenuPlacement.TopNavbar)) goto AssignRoles;

            // Company
            var company = new Menu { Title = "Company", Code = "NAV_COMPANY", Route = "/company", Placement = MenuPlacement.TopNavbar, OrderNo = 1,
                FeaturedTitle = "Sustainability 2026", FeaturedDescription = "Discover our zero-waste initiative and green future roadmap.", FeaturedLinkText = "Read Report" };
            context.Menus.Add(company); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "About Us", Code = "NAV_COMPANY_ABOUT", Route = "/about", ParentId = company.Id, Icon = "building-2", Description = "Our journey and mission", Placement = MenuPlacement.TopNavbar, OrderNo = 1 },
                new Menu { Title = "Core Values", Code = "NAV_COMPANY_VALUES", Route = "/values", ParentId = company.Id, Icon = "target", Description = "Sustainability and integrity", Placement = MenuPlacement.TopNavbar, OrderNo = 2 },
                new Menu { Title = "Leadership", Code = "NAV_COMPANY_LEADERS", Route = "/leadership", ParentId = company.Id, Icon = "users", Description = "Meet our visionaries", Placement = MenuPlacement.TopNavbar, OrderNo = 3 },
                new Menu { Title = "Locations", Code = "NAV_COMPANY_LOC", Route = "/locations", ParentId = company.Id, Icon = "map-pin", Description = "Hassan & Karnataka", Placement = MenuPlacement.TopNavbar, OrderNo = 4 }
            );

            // Products
            var products = new Menu { Title = "Products", Code = "NAV_PRODUCTS", Route = "/products", Placement = MenuPlacement.TopNavbar, OrderNo = 2,
                FeaturedTitle = "PaperX Premium", FeaturedDescription = "Our new FSC-certified specialty range is now available for enterprise.", FeaturedLinkText = "View Catalog" };
            context.Menus.Add(products); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Office & Copier", Code = "NAV_PROD_OFFICE", Route = "/explore/office-copier", ParentId = products.Id, Icon = "briefcase", Description = "Modern high-speed workspace paper", Placement = MenuPlacement.TopNavbar, OrderNo = 1 },
                new Menu { Title = "Writing & Printing", Code = "NAV_PROD_WRITE", Route = "/explore/writing-printing", ParentId = products.Id, Icon = "pen-tool", Description = "Premium creative surfaces", Placement = MenuPlacement.TopNavbar, OrderNo = 2 },
                new Menu { Title = "Coated Paper", Code = "NAV_PROD_COAT", Route = "/explore/coated-boards", ParentId = products.Id, Icon = "layers", Description = "High-gloss luxury finishes", Placement = MenuPlacement.TopNavbar, OrderNo = 3 },
                new Menu { Title = "Packaging Solutions", Code = "NAV_PROD_PACK", Route = "/explore/packaging-board", ParentId = products.Id, Icon = "package", Description = "Durable and sustainable protection", Placement = MenuPlacement.TopNavbar, OrderNo = 4 },
                new Menu { Title = "Carton & Labels", Code = "NAV_PROD_CARTON", Route = "/explore/carton-labels", ParentId = products.Id, Icon = "cpu", Description = "Precision retail packaging", Placement = MenuPlacement.TopNavbar, OrderNo = 5 },
                new Menu { Title = "Tissue & Hygiene", Code = "NAV_PROD_TISSUE", Route = "/explore/tissue-hygiene", ParentId = products.Id, Icon = "smile", Description = "Institutional softness and purity", Placement = MenuPlacement.TopNavbar, OrderNo = 6 }
            );

            // Enriching Lives
            var enriching = new Menu { Title = "Enriching Lives", Code = "NAV_ENRICHING", Route = "/enriching-lives", Placement = MenuPlacement.TopNavbar, OrderNo = 3,
                FeaturedTitle = "Community Impact", FeaturedDescription = "How we empowered 50+ local paper artisans this quarter.", FeaturedLinkText = "Learn More" };
            context.Menus.Add(enriching); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "CSR Initiatives", Code = "NAV_ENR_CSR", Route = "/csr", ParentId = enriching.Id, Icon = "heart", Description = "Giving back to society", Placement = MenuPlacement.TopNavbar, OrderNo = 1 },
                new Menu { Title = "Community", Code = "NAV_ENR_COM", Route = "/community", ParentId = enriching.Id, Icon = "smile", Description = "Localized support systems", Placement = MenuPlacement.TopNavbar, OrderNo = 2 },
                new Menu { Title = "Environment", Code = "NAV_ENR_ENV", Route = "/environment", ParentId = enriching.Id, Icon = "leaf", Description = "Committed to zero waste", Placement = MenuPlacement.TopNavbar, OrderNo = 3 }
            );

            // Investors
            var investors = new Menu { Title = "Investors", Code = "NAV_INVESTORS", Route = "/investors", Placement = MenuPlacement.TopNavbar, OrderNo = 4,
                FeaturedTitle = "Q1 FY26 Results", FeaturedDescription = "Transparency and growth. Access our latest shareholders presentation.", FeaturedLinkText = "Download Presentation" };
            context.Menus.Add(investors); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Financials", Code = "NAV_INV_FIN", Route = "/financials", ParentId = investors.Id, Icon = "bar-chart-3", Description = "Quarterly and annual results", Placement = MenuPlacement.TopNavbar, OrderNo = 1 },
                new Menu { Title = "Stock Info", Code = "NAV_INV_STK", Route = "/stock", ParentId = investors.Id, Icon = "trending-up", Description = "Real-time performance data", Placement = MenuPlacement.TopNavbar, OrderNo = 2 },
                new Menu { Title = "Governance", Code = "NAV_INV_GOV", Route = "/governance", ParentId = investors.Id, Icon = "shield-check", Description = "Ethical business practices", Placement = MenuPlacement.TopNavbar, OrderNo = 3 }
            );

            // Media
            var media = new Menu { Title = "Media", Code = "NAV_MEDIA", Route = "/media", Placement = MenuPlacement.TopNavbar, OrderNo = 5,
                FeaturedTitle = "Brand Portal", FeaturedDescription = "Access high-resolution assets and media kits for PaperaX.", FeaturedLinkText = "Visit Portal" };
            context.Menus.Add(media); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "News", Code = "NAV_MED_NEWS", Route = "/news", ParentId = media.Id, Icon = "newspaper", Description = "Latest announcements", Placement = MenuPlacement.TopNavbar, OrderNo = 1 },
                new Menu { Title = "Gallery", Code = "NAV_MED_GAL", Route = "/gallery", ParentId = media.Id, Icon = "image", Description = "Captured moments of excellence", Placement = MenuPlacement.TopNavbar, OrderNo = 2 },
                new Menu { Title = "Assets", Code = "NAV_MED_AST", Route = "/assets", ParentId = media.Id, Icon = "file-text", Description = "Official brand guidelines", Placement = MenuPlacement.TopNavbar, OrderNo = 3 }
            );

            // Careers
            var careers = new Menu { Title = "Careers", Code = "NAV_CAREERS", Route = "/careers", Placement = MenuPlacement.TopNavbar, OrderNo = 6,
                FeaturedTitle = "Life at PaperaX", FeaturedDescription = "Voted Top 10 Sustainable Employers of 2025. Join our mission.", FeaturedLinkText = "Apply Now" };
            context.Menus.Add(careers); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Openings", Code = "NAV_CAR_OPN", Route = "/jobs", ParentId = careers.Id, Icon = "user-plus", Description = "Join our growing family", Placement = MenuPlacement.TopNavbar, OrderNo = 1 },
                new Menu { Title = "Culture", Code = "NAV_CAR_CUL", Route = "/culture", ParentId = careers.Id, Icon = "compass", Description = "Work-life at PaperaX", Placement = MenuPlacement.TopNavbar, OrderNo = 2 },
                new Menu { Title = "Gallery", Code = "NAV_CAR_GAL", Route = "/life-gallery", ParentId = careers.Id, Icon = "camera", Description = "Life in action at PaperaX", Placement = MenuPlacement.TopNavbar, OrderNo = 3 },
                new Menu { Title = "Internships", Code = "NAV_CAR_INT", Route = "/internships", ParentId = careers.Id, Icon = "graduation-cap", Description = "Shape your future here", Placement = MenuPlacement.TopNavbar, OrderNo = 4 }
            );
            
            await context.SaveChangesAsync();

            await context.SaveChangesAsync();

        AssignRoles:
            // Assign all navbar menus to guest, admin, and customer
            var navbarMenus = await context.Menus.Where(m => m.Placement == MenuPlacement.TopNavbar).ToListAsync();
            foreach (var m in navbarMenus)
            {
                AssignRolesToMenu(context, m, guestRole, adminRole, customerRole);
            }
            await context.SaveChangesAsync();
        }

        private static async Task SeedAdminSidebarMenusAsync(ApplicationDbContext context, Role adminRole)
        {
            if (await context.Menus.AnyAsync(m => m.Placement == MenuPlacement.Sidebar && m.Code.StartsWith("ADMIN_"))) goto AssignRoles;

            var dashboard = new Menu { Title = "Dashboard", Code = "ADMIN_DASHBOARD", Route = "/admin/dashboard", Icon = "layout-dashboard", Placement = MenuPlacement.Sidebar, OrderNo = 1 };
            
            var catalog = new Menu { Title = "Catalog", Code = "ADMIN_CATALOG", Icon = "package", Placement = MenuPlacement.Sidebar, OrderNo = 2 };
            context.Menus.AddRange(dashboard, catalog); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Products", Code = "ADMIN_PROD", Route = "/admin/products", ParentId = catalog.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Categories", Code = "ADMIN_CAT", Route = "/admin/categories", ParentId = catalog.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 },
                new Menu { Title = "Brands", Code = "ADMIN_BRANDS", Route = "/admin/brands", ParentId = catalog.Id, Placement = MenuPlacement.Sidebar, OrderNo = 3 },
                new Menu { Title = "Product Reviews", Code = "ADMIN_REVIEWS", Route = "/admin/reviews", ParentId = catalog.Id, Placement = MenuPlacement.Sidebar, OrderNo = 4 }
            );

            var orders = new Menu { Title = "Orders", Code = "ADMIN_ORDERS_GRP", Icon = "shopping-bag", Placement = MenuPlacement.Sidebar, OrderNo = 3 };
            context.Menus.Add(orders); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "All Orders", Code = "ADMIN_ORDERS", Route = "/admin/orders", ParentId = orders.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Returns & Refunds", Code = "ADMIN_RETURNS", Route = "/admin/returns", ParentId = orders.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 }
            );

            var inventory = new Menu { Title = "Inventory", Code = "ADMIN_INV_GRP", Icon = "warehouse", Placement = MenuPlacement.Sidebar, OrderNo = 4 };
            context.Menus.Add(inventory); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "All Inventory", Code = "ADMIN_INV", Route = "/admin/inventory", ParentId = inventory.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Stock Alerts", Code = "ADMIN_STOCK", Route = "/admin/stock-alerts", ParentId = inventory.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 }
            );

            var customers = new Menu { Title = "Customers", Code = "ADMIN_CUST_GRP", Icon = "users", Placement = MenuPlacement.Sidebar, OrderNo = 5 };
            context.Menus.Add(customers); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "All Customers", Code = "ADMIN_CUST", Route = "/admin/customers", ParentId = customers.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Platform Feedback", Code = "ADMIN_FEEDBACK", Route = "/admin/customer-reviews", ParentId = customers.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 }
            );

            var marketing = new Menu { Title = "Marketing", Code = "ADMIN_MKT_GRP", Icon = "tags", Placement = MenuPlacement.Sidebar, OrderNo = 6 };
            context.Menus.Add(marketing); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Coupons", Code = "ADMIN_COUPONS", Route = "/admin/coupons", ParentId = marketing.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Promotions", Code = "ADMIN_PROMO", Route = "/admin/promotions", ParentId = marketing.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 },
                new Menu { Title = "Promotion Types", Code = "ADMIN_PROMO_TYPE", Route = "/admin/promotion-types", ParentId = marketing.Id, Placement = MenuPlacement.Sidebar, OrderNo = 3 },
                new Menu { Title = "Banner Management", Code = "ADMIN_BANNERS", Route = "/admin/banners", ParentId = marketing.Id, Placement = MenuPlacement.Sidebar, OrderNo = 4 }
            );

            var reports = new Menu { Title = "Reports", Code = "ADMIN_REP_GRP", Icon = "file-text", Placement = MenuPlacement.Sidebar, OrderNo = 7 };
            context.Menus.Add(reports); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Sales Reports", Code = "ADMIN_SALES_REP", Route = "/admin/sales-reports", ParentId = reports.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Inventory Reports", Code = "ADMIN_INV_REP", Route = "/admin/inventory-reports", ParentId = reports.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 }
            );

            var setting = new Menu { Title = "Setting", Code = "ADMIN_SET_GRP", Icon = "settings", Placement = MenuPlacement.Sidebar, OrderNo = 8 };
            context.Menus.Add(setting); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Account", Code = "ADMIN_SET_ACC", Route = "/admin/settings/account", ParentId = setting.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Notifications", Code = "ADMIN_SET_NOT", Route = "/admin/settings/notifications", ParentId = setting.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 },
                new Menu { Title = "Theme", Code = "ADMIN_SET_THM", Route = "/admin/settings/theme", ParentId = setting.Id, Placement = MenuPlacement.Sidebar, OrderNo = 3 },
                new Menu { Title = "Menu Management", Code = "ADMIN_MENUS", Route = "/admin/menus", ParentId = setting.Id, Placement = MenuPlacement.Sidebar, OrderNo = 4 }
            );

            await context.SaveChangesAsync();

            await context.SaveChangesAsync();

        AssignRoles:
            // Fix ADMIN_MENUS parent (migrate out of Setting)
            var adminMenuMgt = await context.Menus.FirstOrDefaultAsync(m => m.Code == "ADMIN_MENUS");
            if (adminMenuMgt != null && adminMenuMgt.ParentId != null)
            {
                adminMenuMgt.ParentId = null;
                adminMenuMgt.OrderNo = 9;
                adminMenuMgt.Icon = "menu";
                await context.SaveChangesAsync();
            }

            var adminMenus = await context.Menus.Where(m => m.Placement == MenuPlacement.Sidebar && m.Code.StartsWith("ADMIN_")).ToListAsync();
            foreach (var m in adminMenus)
            {
                AssignRolesToMenu(context, m, adminRole);
            }
            await context.SaveChangesAsync();
        }

        private static async Task SeedCustomerSidebarMenusAsync(ApplicationDbContext context, Role customerRole)
        {
            if (await context.Menus.AnyAsync(m => m.Placement == MenuPlacement.Sidebar && m.Code.StartsWith("CUST_"))) goto AssignRoles;

            var dashboard = new Menu { Title = "Dashboard", Code = "CUST_DASHBOARD", Route = "/portal/dashboard", Icon = "layout-dashboard", Placement = MenuPlacement.Sidebar, OrderNo = 1 };
            
            var shopping = new Menu { Title = "Shopping", Code = "CUST_SHOP_GRP", Icon = "package", Placement = MenuPlacement.Sidebar, OrderNo = 2 };
            context.Menus.AddRange(dashboard, shopping); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Browse Products", Code = "CUST_PROD", Route = "/portal/products", ParentId = shopping.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Categories", Code = "CUST_CAT", Route = "/portal/categories", ParentId = shopping.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 },
                new Menu { Title = "New Arrivals", Code = "CUST_NEW", Route = "/portal/new-arrivals", ParentId = shopping.Id, Placement = MenuPlacement.Sidebar, OrderNo = 3 },
                new Menu { Title = "Offers", Code = "CUST_OFFERS", Route = "/portal/offers", ParentId = shopping.Id, Placement = MenuPlacement.Sidebar, OrderNo = 4 }
            );

            var orders = new Menu { Title = "Orders", Code = "CUST_ORD_GRP", Icon = "shopping-bag", Placement = MenuPlacement.Sidebar, OrderNo = 3 };
            context.Menus.Add(orders); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "My Orders", Code = "CUST_ORD", Route = "/portal/orders", ParentId = orders.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Track Orders", Code = "CUST_TRK", Route = "/portal/track-orders", ParentId = orders.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 }
            );

            var returns = new Menu { Title = "Returns", Code = "CUST_RET_GRP", Icon = "refresh-ccw", Placement = MenuPlacement.Sidebar, OrderNo = 4 };
            context.Menus.Add(returns); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "My Returns", Code = "CUST_RET_MY", Route = "/portal/my-returns", ParentId = returns.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Return Tracking", Code = "CUST_RET_TRK", Route = "/portal/returns", ParentId = returns.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 }
            );

            var wishlist = new Menu { Title = "Wishlist", Code = "CUST_WISH", Route = "/portal/wishlist", Icon = "heart", Placement = MenuPlacement.Sidebar, OrderNo = 5 };

            var support = new Menu { Title = "Support", Code = "CUST_SUP_GRP", Icon = "help-circle", Placement = MenuPlacement.Sidebar, OrderNo = 6 };
            context.Menus.AddRange(wishlist, support); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Help Center", Code = "CUST_HLP", Route = "/portal/help", ParentId = support.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Contact Support", Code = "CUST_CONT", Route = "/portal/contact", ParentId = support.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 }
            );

            var setting = new Menu { Title = "Setting", Code = "CUST_SET_GRP", Icon = "settings", Placement = MenuPlacement.Sidebar, OrderNo = 7 };
            context.Menus.Add(setting); await context.SaveChangesAsync();
            context.Menus.AddRange(
                new Menu { Title = "Account", Code = "CUST_SET_ACC", Route = "/portal/settings/account", ParentId = setting.Id, Placement = MenuPlacement.Sidebar, OrderNo = 1 },
                new Menu { Title = "Notifications", Code = "CUST_SET_NOT", Route = "/portal/settings/notifications", ParentId = setting.Id, Placement = MenuPlacement.Sidebar, OrderNo = 2 },
                new Menu { Title = "Theme", Code = "CUST_SET_THM", Route = "/portal/settings/theme", ParentId = setting.Id, Placement = MenuPlacement.Sidebar, OrderNo = 3 }
            );

            await context.SaveChangesAsync();

            await context.SaveChangesAsync();

        AssignRoles:
            var custMenus = await context.Menus.Where(m => m.Placement == MenuPlacement.Sidebar && m.Code.StartsWith("CUST_")).ToListAsync();
            foreach (var m in custMenus)
            {
                AssignRolesToMenu(context, m, customerRole);
            }
            await context.SaveChangesAsync();
        }

        private static void AssignRolesToMenu(ApplicationDbContext context, Menu menu, params Role[] roles)
        {
            foreach (var role in roles)
            {
                if (!context.MenuRoles.Any(mr => mr.MenuId == menu.Id && mr.RoleId == role.Id))
                {
                    context.MenuRoles.Add(new MenuRole { MenuId = menu.Id, RoleId = role.Id });
                }
            }
        }
    }
}
