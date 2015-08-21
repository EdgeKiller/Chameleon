TITLE "Underage verification"
PRINT "Enter your age :"
INPUT age
CLEAR 

IF age => 18 THEN over

under:
PRINT "You cant enter this, because you're minor."
WAIT
EXIT

over:
PRINT "Welcome !"
WAIT
EXIT
