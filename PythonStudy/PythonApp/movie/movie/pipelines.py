# -*- coding: utf-8 -*-

# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://doc.scrapy.org/en/latest/topics/item-pipeline.html

import json

class MoviePipeline(object):
    def __init__(self):
        self.filename=open("my_meiju.txt",'w')
        #self.filename.write('123'+'\n')

    def process_item(self, item, spider):      
          #name =item['name'].encode('utf-8')
          name = json.dumps(dict(item),ensure_ascii=False)+',\n'
          #name = name.encode('utf-8')  #不可用utf-8转换 否则报错
          self.filename.write(name)         

    def close_spider(self,spider):
        self.filename.close()