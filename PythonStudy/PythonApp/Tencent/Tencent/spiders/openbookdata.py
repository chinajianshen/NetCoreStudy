# -*- coding: utf-8 -*-
import scrapy
from Tencent.items import OpenBookDataItem
import time

class openbookdataSpider(scrapy.Spider):
    name="openbookdata"
    allowed_domains=["openbookdata.com.cn"]
    url="http://www.openbookdata.com.cn/Marketing/RinkinginfoList.aspx"
    start_urls=[url]

    def parse(self, response):
        #return super().parse(response)
        for each in response.xpath("//ul[@class='newslist clearfix']"):
            item = OpenBookDataItem()
            item["title"]=each.xpath("./li/text()").extract()[0]
            item["url"]=each.xpath("./li/text()").extract()[0]

            yield item

        #yield scrapy.Request(self.url, callback = self.parse)

