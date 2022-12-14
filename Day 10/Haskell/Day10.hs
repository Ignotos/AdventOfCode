import Data.List.Split (splitOn)

data CpuInstruction = Addx Int | Noop deriving Show

parseAddx = Addx . (\x -> read x::Int) . last . splitOn " "

parseSignal signal
    | signal == "noop" = Noop
    | otherwise        = parseAddx signal 

getSignalStrength register cycle
    | elem cycle [20, 60, 100, 140, 180, 220] = register * cycle
    | otherwise                               = 0

getPixel spritePos col
    | elem col [spritePos - 1, spritePos, spritePos + 1] = '#'
    | otherwise                                          = '.'

-- r:  register
-- c:  cycle
-- is: instructions
getSignalStrengths _ _ _ []           = []
getSignalStrengths r c (Just i) is    = getSignalStrength r c : getSignalStrengths (r + i) (c + 1) Nothing is
getSignalStrengths r c Nothing (i:is) = 
    case i of
        Noop       -> getSignalStrength r c : getSignalStrengths r (c + 1) Nothing is
        Addx value -> getSignalStrength r c : getSignalStrengths r (c + 1) (Just value) is

-- s:  sprite position
-- c:  column
-- is: instructions
drawPixels _ _ _ []           = []
drawPixels s 40 i is          = '\n' : drawPixels s 0 i is
drawPixels s c (Just i) is    = getPixel s c : drawPixels (s + i) (c + 1) Nothing is
drawPixels s c Nothing (i:is) = 
    case i of
        Noop       -> getPixel s c : drawPixels s (c + 1) Nothing is
        Addx value -> getPixel s c : drawPixels s (c + 1) (Just value) is

main = do
    input <- readFile "..\\input.txt"
    let instructions = map parseSignal $ lines input
    -- #1
    print $ sum $ getSignalStrengths 1 1 Nothing instructions
    -- #2
    putStrLn $ drawPixels 1 0 Nothing instructions