jQuery(document).ready(function () {
    $.ajax({
        url: "/Account/GetMenu",
        type: "post",
        dataType: "json",
        async: false,
        success: function (res) {
            //var menus = data.menu;
            //var arrMenus = [];
            //$.each(menus, function (i) {
            //    var row = menus[i];
            //    if (row.N_LEVEL == 0) {
            //        var arrMenu = {};
            //        arrMenu.id = row.C_MENU_ID;
            //        arrMenu.isOpen = false;
            //        arrMenu.text = row.V_MENU_NAME;
            //        arrMenu.icon = 'fa fa-circle-o';
            //        arrMenu.children = [];
            //        var arrChildren = $.fn.jsonWhere(menus, function (v) { return v.C_FATHER_ID == row.C_MENU_ID && v.N_LEVEL == 1 });
            //        if (arrChildren.length > 0) {
            //            $.each(arrChildren, function (i) {
            //                var arrChildrenMenu = {};
            //                arrChildrenMenu.id = arrChildren[i].C_MENU_ID;
            //                arrChildrenMenu.text = arrChildren[i].V_MENU_NAME;
            //                arrChildrenMenu.url = arrChildren[i].V_MENU_URL.substring(7).replace('.aspx','');
            //                arrChildrenMenu.targetType = 'iframe-tab';
            //                arrChildrenMenu.icon = 'fa fa-list';
            //                arrChildrenMenu.children = [];
            //                var arrSubChildren = $.fn.jsonWhere(menus, function (v) { return v.C_FATHER_ID == arrChildren[i].C_MENU_ID && v.N_LEVEL == 2 });
            //                if (arrSubChildren.length > 0) {
            //                    $.each(arrSubChildren, function (i) {
            //                        var arrSubChildrenMenu = {};
            //                        arrSubChildrenMenu.id = arrSubChildren[i].C_MENU_ID;
            //                        arrSubChildrenMenu.text = arrSubChildren[i].V_MENU_NAME;
            //                        arrSubChildrenMenu.url = arrSubChildren[i].V_MENU_URL;
            //                        arrSubChildrenMenu.targetType = 'iframe-tab';
            //                        arrSubChildrenMenu.icon = 'fa fa-list';
            //                        arrChildrenMenu.children.push(arrSubChildrenMenu);
            //                    });
            //                }
            //                arrMenu.children.push(arrChildrenMenu);

            //            });
            //        }
            //        arrMenus.push(arrMenu);
            //    }
            //});
            $('.sidebar-menu').sidebarMenu({ data: res.data, param: { strUser: 'admin' } });
            addTabs(({ id: '10000', title: '欢迎页', close: false, url: '/Home/WelCome' }));
            App.fixIframeCotent();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            dialogMsg(errorThrown, -1);
        }
    });
});