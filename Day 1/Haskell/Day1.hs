import Data.List (sort)

parseCalories _ [] = [] 
parseCalories total (calorie:calories) 
    | calorie == [] = total : parseCalories 0 calories
    | otherwise     = parseCalories (total + read calorie::Int) calories 

main = do
    input <- readFile "..\\input.txt"
    let calories = parseCalories 0 $ lines input
    -- #1
    print $ maximum calories
    -- #2 
    print $ sum $ take 3 $ reverse $ sort calories