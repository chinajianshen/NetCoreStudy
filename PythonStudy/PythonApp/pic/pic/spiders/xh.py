# -*- coding: utf-8 -*-
import scrapy
from pic.items import PicItem


class XhSpider(scrapy.Spider):
    # 爬虫名称，唯一
    name = 'xh'
    # 允许访问的域
    allowed_domains = ['xiaohuar.com']
    # 初始URL
    start_urls = ['http://www.xiaohuar.com/list-1-1.html']
    domain="http://www.xiaohuar.com"

    def parse(self, response):
        #获取所有图片的a标签
        allpics = response.xpath('//div[@class="img"]/a')
        for pic in allpics:
            item = PicItem()
            name = pic.xpath("./img/@alt").extract()[0]
            addr=pic.xpath("./img/@src").extract()[0]
            if addr.find('http') == -1:
                addr = self.domain + addr
            item['name']=name
            item["addr"]=addr
            # 返回爬取到的数据
            yield item