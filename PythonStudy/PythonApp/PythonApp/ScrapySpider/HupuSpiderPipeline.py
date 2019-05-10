import scrapy
class HupuSpiderPipeline(object):
    def process_item(self,item,spider):
        if not item['title']:
            # 如果这个pipeline抛出DropItem异常，那么这个item就不会传给后面的pipeline了
            raise DropItem("invalid item")
        title=item['title']
        print(title)
        # return后 会把这个item继续传给后面的pipeline
        return item