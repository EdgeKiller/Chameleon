TITLE "Calculator with Chameleon"
PRINT "Type the first number : "
INPUT num1
PRINT "Type the second number : "
INPUT num2
PRINT "Type operator : "
INPUT op

IF (op == "+") THEN ADD ' Goto ADD
IF (op == "-") THEN REMOVE ' Goto REMOVE
IF (op == "*") THEN MULTIPLY ' Goto MULTIPLY
IF (op == "/") THEN DIVIDE ' Goto DIVIDE
IF (op == "%") THEN MODULO ' Goto MODULO
IF (op == "^") THEN POW ' Goto POW
PRINT "Bad operator"
WAIT
EXIT

ADD:
result = (num1 + num2)
GOTO END

REMOVE:
result = (num1 - num2)
GOTO END

MULTIPLY:
result = (num1 * num2)
GOTO END

DIVIDE:
result = (num1 / num2)
GOTO END

MODULO:
result = (num1 % num2)
GOTO END

POW:
result = (num1 ^ num2)
GOTO END

END:
PRINT "Result : " + result
WAIT
EXIT 