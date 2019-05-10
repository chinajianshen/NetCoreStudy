# -*- coding: utf-8 -*-
import scrapy
from movie.items import MovieItem
import json

#编写爬虫
class MeijuSpider(scrapy.Spider):
    name = 'meiju'
    allowed_domains = ['meijutt.com']
    start_urls = ['http://www.meijutt.com/new100.html']  

    def parse(self, response):        
        movies = response.xpath('//ul[@class="top-list  fn-clear"]/li')
        for each_movie in movies:
            item = MovieItem()
            item['name']=each_movie.xpath('./h5/a/@title').extract()[0]
            item['url']=each_movie.xpath("./h5/a/@href").extract()[0]
            yield item