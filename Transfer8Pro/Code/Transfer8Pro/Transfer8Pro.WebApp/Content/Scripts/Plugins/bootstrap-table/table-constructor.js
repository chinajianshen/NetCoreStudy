var TableInit = function (tableObj) {
    var oTableInit;

    if (tableObj) {
        oTableInit = tableObj;
    } else {
        oTableInit = new Object();
    }

    var tableId;
    var serverUrl;
    var loadingIndex;

    oTableInit.columns = [];

    oTableInit.calBack = function () { }

    oTableInit.validateSession = function (json) {
        if (json.Status && json.Status == -1) {
            var alertLayer = layer.alert(json.Msg, function () {
                layer.close(alertLayer);
                window.location.href = "/auth/index";
            });
            return;
        }

        return oTableInit.calBack(json);
    }

    oTableInit.fetchParams = function (obj) {
        if (!obj) {
            obj = {};
        }
        $.each($('.data-param'),
            function (index, item) {
                obj[item.name] = item.value;
            });
        return JSON.stringify(obj);
    }

    oTableInit.refresh = function() {
        $(tableId).bootstrapTable('refresh', { url: serverUrl });
        $('#all').iCheck('uncheck');
    }

    oTableInit.postColumns = function() {
        var res = [];
        $.each(oTableInit.columns,
            function (index, item) {
                if (!item.unpost) {
                    var data = { field: item.field, title: item.title };
                    res.push(data);
                }

            });
        return res;
    }

    //初始化Table
    oTableInit.Init = function (paramObj) {
        tableId = paramObj.tableId;
        serverUrl = paramObj.url;
        $(paramObj.tableId).bootstrapTable({
            url: paramObj.url, //'/User/UserList',         //请求后台的URL（*）
            method: typeof paramObj.method == "undefined" ? "POST" : paramObj.method,//'post'
            toolbar: typeof paramObj.toolbar == "undefined" ? '' : paramObj.toolbar,  //'#searchPanel', //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: typeof paramObj.pagination == "undefined" ? true : paramObj.pagination,                   //是否显示分页（*）
            sortable: typeof paramObj.sortable == "undefined" ? false : paramObj.sortable,                     //是否启用排序
            search: typeof paramObj.search == "undefined" ? false : paramObj.search, 
            showColumns: typeof paramObj.showColumns == "undefined" ? false : paramObj.showColumns, 
            showRefresh: typeof paramObj.showRefresh == "undefined" ? false : paramObj.showRefresh, 
            sortOrder: "desc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            strictSearch: true,
            clickToSelect: true,                //是否启用点击选中行
            //height: 560,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: typeof paramObj.toolbar == "undefined" ? 'id' : paramObj.uniqueId,// "LoginName",                     //每一行的唯一标识，一般为主键列
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: oTableInit.columns,
            onLoadSuccess: function() {
                $('#all').iCheck('uncheck');
            },
            onPostBody: function () {
                if (paramObj.renderCheck) {
                    $('.i-checks').iCheck("destroy");
                    $('.i-checks').iCheck({
                        checkboxClass: 'icheckbox_square-green',
                        radioClass: 'iradio_square-green'
                    });
                    $('#all').on('ifChanged', function (e) {
                        //alert('sss');
                        if (e.currentTarget.checked) {
                            $('.i-checks').not('[disabled=disabled]').iCheck("check");
                        } else {
                            $('.i-checks').not('[disabled=disabled]').iCheck("uncheck");
                        }
                    });
                }

                if (loadingIndex) {
                    layer.close(loadingIndex);
                }

                if ($('.fancybox').fancybox) {
                    $('.fancybox').fancybox({
                        type: 'image',
                        openEffect: 'none',
                        closeEffect: 'none'
                    });
                }
            },
            
            responseHandler: oTableInit.validateSession
        });

        if (paramObj.searchId) {
            $(paramObj.searchId).click(function() {
                $(tableId).bootstrapTable('refresh', { url: serverUrl });
                $('#all').iCheck('uncheck');
            });
        }

        $(paramObj.outputId).click(function () {
            var pams = [];
            var fields = [];
            var pamEntity = JSON.parse(oTableInit.fetchParams());
            var fieldEntity = oTableInit.postColumns();

            $.each(pamEntity,
                function (key, value) {
                    pams.push(key + "=" + value);
                });


            $.each(fieldEntity,
                function (index, item) {
                    fields.push("fields[" + index + "].field=" + item.field + "&fields[" + index + "].title=" + item.title);
                });

            window.open(paramObj.outputUrl + '?' + encodeURI(pams.join('&') + "&" + fields.join('&')));
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        
        var temp = {
            pageSize: params.limit,   //页面大小
            pageIndex: params.offset / params.limit + 1,
            total: total
        };
        //return fetchParams(temp);
        loadingIndex = layer.load(2, {
            content: '<span style="margin-left: -60px;">数据加载中，请稍后...</span>',
            success: function (layero) {
                layero.find('.layui-layer-content').css('width', '200px').css('padding-top', '40px');
            },
            shade: [0.3, '#aaa']
        });
        return oTableInit.fetchParams(temp);
    };


    return oTableInit;
}