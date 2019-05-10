import scrapy

class TestSpider(scrapy.Spider):
    # 定义爬取的域名，如果返回的url所属的域名不在这个列表里面，scrapy就不会去爬取内容
    allowed_domains=['bbs.hupu.com']
     # 定义起始的url
    start_urls=('http://bbs.hupu.com/bxj/',)
    #抓取的详细页面
    spiderPageUrl='http://bbs.hupu.com/bxj-'

    # 如果重写了这个方法，scrapy引擎取到的起始url就是这个方法返回的内容了
    # 这样start_urls就不会生效了
    def start_requests(self):
        for i in range(1,10):
            yield scrapy.Request(self.spiderPageUrl + str(i))

    def parse(self, response):
        # scrapy拉取到url的内容后，会封装成Response对象,然后回调这个parse()方法
	    # 我们可以对这个response进行解析，然后根据策略返回响应的内容
	    # scrapy 自带了xpath的方式解析内容，xpath教程可以看这篇  https://blog.csdn.net/u013332124/article/details/80621638
        title_href=response.xpath(".//a[@class='title']/@href").extract_first()
        title = response.xpath(".//a[@class='title']/text()").extract_first()
        # 返回一个request对象和一个item对象，request对象放的是标题的url，后面scrapy会继续读取这个url然后交给parse继续解析
        return [scrapy.Request(content_url,self.post_content_parse,dont_filter=True),{"title":title}]