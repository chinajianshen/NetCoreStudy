from scrapy.cmdline import execute
#scrapy crawl meiju --nolog  命令行切换到当前窗口 执行爬虫 --nolog不显示日志

if __name__ == '__main__':
    execute(['scrapy', 'crawl', 'meiju']);
