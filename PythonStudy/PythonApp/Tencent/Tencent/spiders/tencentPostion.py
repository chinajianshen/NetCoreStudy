# -*- coding: utf-8 -*-
import scrapy
from Tencent.items import TencentItem
import time

class TencentpostionSpider(scrapy.Spider):
    '''
    name = 'tencentPostion'
    allowed_domains = ['tencent.com']
    start_urls = ['http://tencent.com/']
    '''
    """
    功能：爬取腾讯社招信息
    """
    # 爬虫名
    name = "tencentPosition"
    # 爬虫作用范围
    allowed_domains = ["tencent.com"]
    url = "http://hr.tencent.com/position.php?&start="
    # 起始url
    offset = 0
    start_urls = [url + str(offset)]

    '''
    allowed_domains = ["openbookdata.com.cn"] 
    url="http://www.openbookdata.com.cn/Marketing/MarketIndex.aspx"    
    '''
  

    def parse(self, response):       
        for each in response.xpath("//tr[@class='even'] | //tr[@class='odd']"):
            # 初始化模型对象
            item = TencentItem()
            # 职位名称
            item['positionname'] = each.xpath("./td[1]/a/text()").extract()[0]
            # 详情连接
            item['positionlink'] = each.xpath("./td[1]/a/@href").extract()[0]
            # 职位类别
            item['positionType'] = each.xpath("./td[2]/text()").extract()[0]
            # 招聘人数
            item['peopleNum'] =  each.xpath("./td[3]/text()").extract()[0]
            # 工作地点
            item['workLocation'] = each.xpath("./td[4]/text()").extract()[0]
            # 发布时间
            item['publishTime'] = each.xpath("./td[5]/text()").extract()[0]

            yield item

        if self.offset < 30:
            self.offset += 10

        # 每次处理完一页的数据之后，重新发送下一页页面请求
        # self.offset自增10，同时拼接为新的url，并调用回调函数self.parse处理Response
        yield scrapy.Request(self.url + str(self.offset), callback = self.parse)

    #当指定了URL时，make_requests_from_url() 将被调用来创建Request对象。 该方法仅仅会被Scrapy调用一次，因此您可以将其实现为生成器。
    '''
    def make_requests_from_url(self, url):
        tmp_url= url + str(offset)
        return super().make_requests_from_url(tmp_url)
    '''

    #需要在启动时以POST登录某个网站
    '''
    def start_requests(self):
        return [scrapy.FormRequest("http://www.openbookdata.com.cn/handlers/UserController/UserLogins.ashx",
                                   formdata={'ln':'281349838@qq.com','pw':'123456',"ts": time.time()},
                                   callback=self.logged_in)]

    def logged_in(self,response):
        pass
    '''