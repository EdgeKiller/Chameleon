TITLE "Calculator with Chameleon"
PRINT "Type the first number : "
INPUT num1
PRINT "Type the second number : "
INPUT num2
PRINT "Type operator : "
INPUT op

IF (op == "+") THEN ADD
IF (op == "-") THEN REMOVE
IF (op == "*") THEN MULTIPLY
IF (op == "/") THEN DIVIDE
IF (op == "%") THEN MODULO
IF (op == "^") THEN POW

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