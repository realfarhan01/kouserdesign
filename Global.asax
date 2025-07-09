<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim originalPath As String = HttpContext.Current.Request.Path.ToLower()
        Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToLower()
        Dim PageName As String = sPath.Replace("/", "")
        Dim originalPathAcurate As String = HttpContext.Current.Request.Path
        Dim sPathAcurate As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        Dim PageNameAcurate As String = sPathAcurate.Replace("/", "")
        Dim CallPage As String = ""

        'If originalPath.Contains("/home") Then
        '    CallPage = originalPath.Replace(PageName, "index.aspx")
        '    Context.RewritePath(CallPage, False)
        'End If

        If originalPath.Contains("/pc-") AndAlso Not originalPath.Contains(".jpg") AndAlso Not originalPath.Contains(".png") AndAlso Not originalPath.Contains(".pdf") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "productdetail.aspx?pc=" & PageNameAcurate)
            Context.RewritePath(CallPage, False)
        End If

        If originalPath.Contains("/cat-") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "products.aspx?cat=" & PageNameAcurate.Replace("cat-", ""))
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/about-us") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "about-us.aspx")
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/products") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "products.aspx")
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/contact-us") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "contact-us.aspx")
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/terms-and-conditions") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "terms-and-conditions.aspx")
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/shipping-policy") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "shipping-policy.aspx")
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/refund-policy") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "refund-policy.aspx")
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/cancellation-policy") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "cancellation-policy.aspx")
            Context.RewritePath(CallPage, False)
        ElseIf originalPath.Contains("/categories-sample") Then
            CallPage = originalPathAcurate.Replace(PageNameAcurate, "categories-sample.aspx")
            Context.RewritePath(CallPage, False)
        End If
    End Sub
</script>