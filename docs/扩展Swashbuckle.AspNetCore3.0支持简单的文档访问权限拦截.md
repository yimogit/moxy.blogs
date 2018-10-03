## Swashbuckle.AspNetCore3.0 介绍

> 一个使用 ASP.NET Core 构建的 API 的 Swagger 工具。直接从您的路线，控制器和模型生成漂亮的 API 文档，包括用于探索和测试操作的 UI。  
> 项目主页：https://github.com/domaindrivendev/Swashbuckle.AspNetCore  
> 项目官方示例：https://github.com/domaindrivendev/Swashbuckle.AspNetCore/tree/master/test/WebSites
> 继上篇[Swashbuckle.AspNetCore3.0 的二次封装与使用](https://www.cnblogs.com/morang/p/9740190.html)分享了二次封装的代码，本篇将分享如何给文档添加一个登录页，控制文档的访问权限(文末附完整 Demo)

## 关于生产环境接口文档的显示

在此之前的接口项目中，若使用了 Swashbuckle.AspNetCore，都是控制其只在开发环境使用，不会就这样将其发布到生产环境(安全第一) 。
那么，怎么安全的发布 swagger 呢？我有两种想法

- 1. 将路由前缀改得超级复杂
- 2. 添加一个拦截器控制 swagger 文档的访问必须获得授权(登录)

大佬若有更好的想法，还望指点一二

下面我将介绍基于 asp.net core2.1 且使用了 Swashbuckle.AspNetCore3.0 的项目种是怎么去实现安全校验的  
通过本篇文章之后，可以放心的将项目中的 swagger 文档发布到生产环境，并使其可通过用户名密码去登录访问,得以安全且方便的测试接口。

## 实现思路

前面已经说到，需要一个拦截器，而这个拦截器还需要是全局的，在 asp.net core 中，自然就需要用到的是[中间件](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1)了

步骤如下，在 UseSwagger 之前使用自定义的中间件  
拦截所有 swagger 相关请求，判断是否授权登录  
若未登录则跳转到授权登录页，登录后即可访问 swagger 的资源

如果项目本身有登录系统，可在自定义中间件中使用项目中的登录，
没有的话，我会分享一个简单的用户密码登录的方案

Demo 入下图所示

![图片](https://dn-coding-net-production-pp.qbox.me/b05ad408-74b6-48c1-9cbc-73f1a529d272.gif)

## 为使用 Swashbuckle.AspNetCore3 的项目添加接口文档登录功能

在写此功能之前，已经[封装了一部分代码](https://www.cnblogs.com/morang/p/9740190.html)，此功能算是在此之前的代码封装的一部分，不过是后面完成的。

### 定义模型存放用户密码

```cs
    public class CustomSwaggerAuth
    {
        public CustomSwaggerAuth() { }
        public CustomSwaggerAuth(string userName,string userPwd)
        {
            UserName = userName;
            UserPwd = userPwd;
        }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        //加密字符串
        public string AuthStr
        {
            get
            {
                return SecurityHelper.HMACSHA256(UserName + UserPwd);
            }
        }
    }
```

### 加密方法(HMACSHA256)

```cs
    public static string HMACSHA256(string srcString, string key="abc123")
    {
        byte[] secrectKey = Encoding.UTF8.GetBytes(key);
        using (HMACSHA256 hmac = new HMACSHA256(secrectKey))
        {
            hmac.Initialize();

            byte[] bytes_hmac_in = Encoding.UTF8.GetBytes(srcString);
            byte[] bytes_hamc_out = hmac.ComputeHash(bytes_hmac_in);

            string str_hamc_out = BitConverter.ToString(bytes_hamc_out);
            str_hamc_out = str_hamc_out.Replace("-", "");

            return str_hamc_out;
        }
    }
```

### 自定义中间件

> 此中间件中有使用的 login.html，其属性均为内嵌资源，故事用 GetManifestResourceStream 读取文件流并输出，这样可以方便的将其进行封装到独立的类库中，而不与输出项目耦合
> 关于退出按钮，可以参考前文自定义 index.html

```cs
    private const string SWAGGER_ATUH_COOKIE = nameof(SWAGGER_ATUH_COOKIE);
    public void Configure(IApplicationBuilder app)
    {
        var options=new {
            RoutePrefix="swagger",
            SwaggerAuthList = new List<CustomSwaggerAuth>()
            {
                new CustomSwaggerAuth("swaggerloginer","123456")
            },
        }
        var currentAssembly = typeof(CustomSwaggerAuth).GetTypeInfo().Assembly;
        app.Use(async (context, next) =>
        {
            var _method = context.Request.Method.ToLower();
            var _path = context.Request.Path.Value;
            // 非swagger相关请求直接跳过
            if (_path.IndexOf($"/{options.RoutePrefix}") != 0)
            {
                await next();
                return;
            }
            else if (_path == $"/{options.RoutePrefix}/login.html")
            {
                //登录
                if (_method == "get")
                {
                    //读取CustomSwaggerAuth所在程序集内嵌的login.html并输出
                    var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.login.html");
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    context.Response.ContentType = "text/html;charset=utf-8";
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.Body.Write(buffer, 0, buffer.Length);
                    return;
                }
                else if (_method == "post")
                {
                    var userModel = new CustomSwaggerAuth(context.Request.Form["userName"], context.Request.Form["userPwd"]);
                    if (!options.SwaggerAuthList.Any(e => e.UserName == userModel.UserName && e.UserPwd == userModel.UserPwd))
                    {
                        await context.Response.WriteAsync("login error!");
                        return;
                    }
                    //context.Response.Cookies.Append("swagger_auth_name", userModel.UserName);
                    context.Response.Cookies.Append(SWAGGER_ATUH_COOKIE, userModel.AuthStr);
                    context.Response.Redirect($"/{options.RoutePrefix}");
                    return;
                }
            }
            else if (_path == $"/{options.RoutePrefix}/logout")
            {
                //退出
                context.Response.Cookies.Delete(SWAGGER_ATUH_COOKIE);
                context.Response.Redirect($"/{options.RoutePrefix}/login.html");
                return;
            }
            else
            {
                //若未登录则跳转登录
                if (!options.SwaggerAuthList.Any(s => !string.IsNullOrEmpty(s.AuthStr) && s.AuthStr == context.Request.Cookies[SWAGGER_ATUH_COOKIE]))
                {
                    context.Response.Redirect($"/{options.RoutePrefix}/login.html");
                    return;
                }
            }
            await next();
        });
        app.UseSwagger();
        app.UseSwaggerUI(c=>{
            if (options.SwaggerAuthList.Count > 0)
            {
                //index.html中添加ConfigObject属性
                c.ConfigObject["customAuth"] = true;
                c.ConfigObject["loginUrl"] = $"/{options.RoutePrefix}/login.html";
                c.ConfigObject["logoutUrl"] = $"/{options.RoutePrefix}/logout";
            }
        });
        app.UseMvc();
    }
```

### index.html 添加退出按钮

- [自定义 index.html 文档](https://github.com/domaindrivendev/Swashbuckle.AspNetCore#customize-indexhtml)
- [默认 index.html](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerUI/index.html)

```js
if (configObject.customAuth) {
  var logOutEle = document.createElement('button')
  logOutEle.className = 'btn '
  logOutEle.innerText = '退出'
  logOutEle.onclick = function() {
    location.href = configObject.logoutUrl
  }
  document.getElementsByClassName('topbar-wrapper')[0].appendChild(logOutEle)
}
```

### login.html

[点此查看](http://www.17sucai.com/pins/30920.html)
可以直接复制使用，或者直接下载文末 demo

## 完整 Demo 下载

博客园下载地址：https://files.cnblogs.com/files/morang/Swashbuckle.AspNetCore3_Demo.zip
