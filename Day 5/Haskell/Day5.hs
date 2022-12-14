parseInt x = read x::Int 
parseMove move = 
    let parts = words move
    in (parseInt (parts !! 1), parseInt (parts !! 3) - 1, parseInt (parts !! 5) - 1)

removeCrates stacks n source =
    let (head, (target:tail)) = splitAt source stacks
    in head ++ [drop n target] ++ tail 

addCrates stacks crates destination = 
    let (head, (target:tail)) = splitAt destination stacks
    in head ++ [crates ++ target] ++ tail

moveCrates1 stacks (n, source, destination) = addCrates (removeCrates stacks n source) (reverse $ take n $ stacks !! source) destination
moveCrates2 stacks (n, source, destination) = addCrates (removeCrates stacks n source) (take n $ stacks !! source) destination

main = do
    input <- readFile "..\\input.txt"
    let stacks = ["NRJTZBDF", "HJNSR", "QFZGJNRC", "QTRGNVF", "FQTL", "NGRBZWCQ", "MHNSLCF", "JTMQND", "SGP"]
    let moves = map parseMove $ drop 10 $ lines input
    -- #1
    mapM putStrLn $ foldl moveCrates1 stacks moves
    -- #2
    putStrLn ""
    mapM putStrLn $ foldl moveCrates2 stacks moves