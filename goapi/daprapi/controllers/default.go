package controllers

import (
	beego "github.com/beego/beego/v2/server/web"
	dapr "github.com/dapr/go-sdk/client"
	"context"
)

type MainController struct {
	beego.Controller
}

func (c *MainController) Get() {
	ctx := context.Background()
	client, err := dapr.NewClient()
	if err != nil{
		c.Data["json"] = err.Error()
		c.ServeJSON()
	}
	// defer client.Close()

	resp, err := client.InvokeMethod(ctx, "pythonapi", "/", "get")
	if err != nil{
		c.Data["json"] = err.Error()
		c.ServeJSON()
	}
	c.Data["json"] = string(resp)
	c.ServeJSON()
	// c.TplName = "index.tpl"
}
