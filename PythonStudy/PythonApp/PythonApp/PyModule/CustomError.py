class MyError(Exception):
    def __init__(self,value):
       self.value=value
    def __str__(self):
        return repr(self.value)

#当创建一个模块有可能抛出多种不同的异常时，一种通常的做法是为这个包建立一个基础异常类，然后基于这个基础类为不同的错误情况创建不同的子类:
class Error(Exception):
    """Base class for exceptions in this module."""
    pass

class InputError(Error):
    """Exception raised for errors in the input.
 
    Attributes:
        expression -- input expression in which the error occurred
        message -- explanation of the error
    """

    def __init__(self, expression,message):
        self.expression=expression
        self.message=message

class TransitionError(Error):
    def __init__(self,previous,next,message):
        self.previous=previous
        self.next=next
        self.message=message

 