import Data.List.Split (splitOn)

data Play = Rock | Paper | Scissors
data Outcome = Win | Lose | Draw

parsePlay [p]
    | elem p "XA" = Rock
    | elem p "YB" = Paper
    | elem p "ZC" = Scissors

parsePlays = map parsePlay . splitOn " "

getPlayValue Rock     = 1
getPlayValue Paper    = 2
getPlayValue Scissors = 3

getOutcome Rock Rock         = Draw
getOutcome Rock Paper        = Win
getOutcome Rock Scissors     = Lose
getOutcome Paper Rock        = Lose
getOutcome Paper Paper       = Draw
getOutcome Paper Scissors    = Win
getOutcome Scissors Rock     = Win
getOutcome Scissors Paper    = Lose
getOutcome Scissors Scissors = Draw

getOutcomeValue Win  = 6
getOutcomeValue Lose = 0
getOutcomeValue Draw = 3

getDesiredOutcome Rock     = Lose
getDesiredOutcome Paper    = Draw
getDesiredOutcome Scissors = Win

getDesiredPlay Rock Win      = Paper
getDesiredPlay Rock Lose     = Scissors
getDesiredPlay Rock Draw     = Rock
getDesiredPlay Paper Win     = Scissors
getDesiredPlay Paper Lose    = Rock
getDesiredPlay Paper Draw    = Paper
getDesiredPlay Scissors Win  = Rock
getDesiredPlay Scissors Lose = Paper
getDesiredPlay Scissors Draw = Scissors

getResult1 [opponent, player] = getOutcomeValue (getOutcome opponent player) + getPlayValue player 
getResult2 [opponent, player] = getOutcomeValue (getDesiredOutcome player) + getPlayValue (getDesiredPlay opponent (getDesiredOutcome player))

main = do
    input <- readFile "..\\input.txt"
    let rounds = map parsePlays $ lines input
    -- #1
    print $ sum $ map getResult1 rounds
    -- #2
    print $ sum $ map getResult2 rounds