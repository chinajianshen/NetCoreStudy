# -*- coding: utf-8 -*-

# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://doc.scrapy.org/en/latest/topics/item-pipeline.html

import urllib3
import os
import requests

class PicPipeline(object):
    def process_item(self, item, spider):       
        dir = os.path.join(os.getcwd(),"images")
        if not os.path.exists(dir):
            os.makedirs(dir)

        headers={'User-Agent': 'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:52.0) Gecko/20100101 Firefox/52.0'}    
        http = urllib3.PoolManager()
        req = http.request("GET",item['addr'],headers=headers)

        file_name = os.path.join(dir,item['name']+'.jpg')
        with open(file_name,'wb') as fp:
            fp.write(req.data)