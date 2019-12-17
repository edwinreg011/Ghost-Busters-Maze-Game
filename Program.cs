using System;
using System.Collections.Generic;

namespace rpg
{
    
    class Human
    {
        public string Name;
        public int Strength;
        public int Intelligence;
        public int Dexterity;
        protected int health;
        public int spCount;
        public List<string> moveSet;
        public string Guild;
         
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }
         
        public Human(string name)
        {
            Guild = "default";
            Name = name;
            spCount = 3;
            Strength = 3;
            Intelligence = 3;
            Dexterity = 3;
            health = 100;
            moveSet = new List<string>();
        }
         
        public Human(string name, int str, int intel, int dex, int hp)
        {
            Guild = "default";
            Name = name;
            Strength = str;
            Intelligence = intel;
            Dexterity = dex;
            health = hp;
            moveSet = new List<string>();
        }
         
        // Build Attack method
        public virtual int Attack(Enemy target)
        {
            int dmg = Strength * 3;
            target.Health -= dmg;
            Console.WriteLine($"{Name} attacked {target.type} for {dmg} damage!");
            return target.Health;
        }

        //shield
        public virtual int Shield(Enemy enemy)
        {
            health = health + enemy.Level * 3;
            Console.WriteLine($"{Name} shielded themselves and blocked the attack!");
            return health;
        }
        //Healer heal
        public virtual int Heal(Human target){
            return 0;
        }
        //Attacker steal
        public virtual int Steal(Enemy target){
            return 0;
        }   
        //Defender meditate
        public virtual int Meditate(){
            return 0;
        }

        //healer ray special
        public virtual int GhostTrap(Enemy target)
        {
            return 0;
        }

        //attacker egon special
        public virtual int CrossTheStreams(Enemy target)
        {
            return 0;
        }

        //defender peter special
        public virtual int ManEatingToaster(Enemy target, Human me)
        {
            return 0;
        }
    }
    

    class Healer : Human
    {
        public Healer(string name) : base(name)
        {
            Guild = "Ray";
            health = 50;
            Intelligence = 25;
            spCount = 3;
            moveSet.Add("Basic Attack");
            moveSet.Add("Heal");
            moveSet.Add("Shield");
            moveSet.Add("Ghost Trap");
        }
        public override int Attack(Enemy target)
        {
            int dmg = Strength * 5;
            target.Health -= dmg;
            //health += health + dmg;
            Console.WriteLine($"{Name} attacked {target.type} for {dmg} damage!");
            return target.Health;
        }
        public override int Heal(Human target) 
        {
            int healed_health = Intelligence * 10;
            target.Health += healed_health;
            Console.WriteLine($"{Name} healed {target.Name} for {healed_health} health!");
            return target.Health;
        }

        public override int GhostTrap(Enemy target)
        {
            if(spCount > 0) {
                int dmg = this.health * 2;
                target.Health -= dmg;
                Console.WriteLine($"{Name} Ghost Trapped {target.type} for {dmg} damage!");
                spCount--;
                return target.Health;
            }
            Console.WriteLine($"{Name} is out of special attacks!");
            return target.Health;
        }


    }

    class Attacker : Human
    {
        public Attacker(string name) : base(name)
        {
            Guild = "Egon";
            Dexterity = 175;
            spCount = 3;
            moveSet.Add("Basic Attack");
            moveSet.Add("Steal");
            moveSet.Add("Cross The Streams");
        }
        public override int Attack(Enemy target)
        {
            int dmg = Dexterity * 5;
            Random rng = new Random();
            int extra = rng.Next(1,6);
            if(extra == 6) {
                dmg += 10;
            }
            target.Health -= dmg;
            Console.WriteLine($"{Name} attacked {target.type} for {dmg} damage!");
            return target.Health;
        }
        public override int Steal(Enemy target) 
        {
            target.Health -= 5;
            Health += 5;
            return target.Health;
        }
        public override int CrossTheStreams(Enemy target)
        {
            if(spCount > 0) {
                Attack(target);
                Attack(target);
                Attack(target);
                spCount--;
                return target.Health;
            } else {
                Console.WriteLine($"{Name} hurt itself in confusion - out of special attacks");
                return target.Health;
            }
            return target.Health;
        }
    }

    class Defender : Human
    {
        public Defender(string name) : base(name)
        {
            Guild = "Peter";
            health = 200;
            spCount = 3;
            moveSet.Add("Basic Attack");
            moveSet.Add("Meditate");
            moveSet.Add("Shield");
            moveSet.Add("Man Eating Toaster");
        }
        public override int Attack(Enemy target)
        {
            base.Attack(target);
            if(target.Health < 50) {
                target.Health = 0;
            }
            return target.Health;
        }
        public override int Meditate()
        {
            health = 200;
            Console.WriteLine($"{Name} healed themselves to full health!");
            return health;
        }
        public override int ManEatingToaster(Enemy target, Human me)
        {
            if(spCount > 0) {
                Random rnd = new Random();
                int isDead = rnd.Next(0,2);
                if(isDead == 1) {
                    me.Health = 0;
                    Console.WriteLine($"{Name}'s got in the way on the Man Eating Toaster and died");
                    return me.Health;
                } else {
                    target.Health = 0;
                    Console.WriteLine($"{Name}'s Man Eating Toaster slayed {target.type}");
                    return target.Health;
                }
            }
            return target.Health;
        }
    }

    class Enemy
    {
        public int Level;
        public int Strength;
        protected int health;
        public string type;
         
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
         
        public Enemy(int level)
        {
            type = "Enemy";
            Level = level;
            health = 100;
            Strength = 5;
        }
         
        // Build Attack method
        public virtual int Attack(Human target)
        {
            int dmg = Strength * 3;
            target.Health -= dmg;
            Console.WriteLine($"{type} attacked {target.Name} for {dmg} damage!");
            return target.Health;
        }
        
    }

    class Nanny : Enemy
    {
        public Nanny(int level) : base(level)
        {
            type = "Ghost Nanny";
            health = 5*level;
            Strength = 2*level;
        }
    }

    class Brothers : Enemy
    {
        public Brothers(int level) : base(level)
        {
            type = "Scoleri Brothers";
            health = 25*level;
            Strength = 9*level;
        }
    }

    class Zuul : Enemy
    {
        public Zuul(int level) : base(level)
        {
            type = "Zuul";
            health = 15*level;
            Strength = 11*level;
        }
    }

    class Slimer : Enemy
    {
        public Slimer(int level) : base(level)
        {
            type = "Slimer";
            health = 20*level;
            Strength = 2*level;
        }
    }

    class Puft : Enemy
    {
        public Puft(int level) : base(level)
        {
            type = "Stay Puft Marshmellow Man";
            health = 100*level;
            Strength = 0*level;
        }
    }
        
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Human player = pickClass();
            Puft enemy = new Puft(500);
            Encounter(player, enemy);
            */
            // MAP GAME
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Human player = pickClass();
            Console.WriteLine($"Now, start your adventure... Good luck, {player.Name}");
            Console.WriteLine($"TIP: Use WASD and 1 2 to play");
            string[,] map = initMap();
            int[] playerCoords = new int[2]{1,1};
            int[] lastLoc = new int[2];
            displayMap(map, playerCoords);
            
            while(true) {
                string keyPressed = Console.ReadLine();
                lastLoc[0] = playerCoords[0];
                lastLoc[1] = playerCoords[1];
                playerCoords = movePlayer(playerCoords, keyPressed, map);
                if(playerCoords[0] == 99) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You Escaped the Haunted House! YOU WIN!");
                    Console.ResetColor();
                    break;
                }
                map = updateMap(map, lastLoc, playerCoords);
                displayMap(map, playerCoords);
                Random rnd = new Random();
                int chance = rnd.Next(0,3);
                //Console.WriteLine(playerCoords[0]);
                //Console.WriteLine(playerCoords[1]);
                //Console.WriteLine(chance);
                
                Nanny a = new Nanny(2);
                Brothers b = new Brothers(4);
                Zuul c = new Zuul(5);
                Slimer d = new Slimer(8);
                Puft e = new Puft(500);
                List<Enemy> randomEnemy = new List<Enemy>();
                randomEnemy.Add(a);
                randomEnemy.Add(b);
                randomEnemy.Add(c);
                randomEnemy.Add(d);
                randomEnemy.Add(e);
            
                
                if(chance == 2) {
                    Random spawnEnemy = new Random();
                    int spawnEnemyNumber = spawnEnemy.Next(0,5);
                    Encounter(player, randomEnemy[spawnEnemyNumber]);
                    displayMap(map, playerCoords);
                }
                if(player.Health <= 0)  {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You Perished the Haunted House...");
                    Console.ResetColor();
                    break;
                }

                
                
            }
            
        }

        static string[,] initMap()
        {
            string [,] map = new string[12,36]
            {
                {"\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680"},
                {"\u2680",".",".",".","\u2680",".",".",".",".",".",".",".",".",".","\u2680",".",".",".",".",".",".",".",".",".",".",".",".","\u2680","\u2680","\u2680",".",".",".",".",".","\u2680"},
                {"\u2680",".",".",".","\u2680",".",".",".",".",".",".",".",".",".","\u2680",".",".",".",".",".",".",".",".",".",".","\u2680","\u2680","\u2680","\u2680","\u2680",".",".",".",".",".","\u2680"},                
                {"\u2680",".",".",".","\u2680",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","\u2680","\u2680","\u2680","\u2680",".",".",".",".",".",".",".",".",".","\u2680"},                
                {"\u2680","\u2680",".","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680",".",".",".",".",".","\u2680","\u2680","\u2680","\u2680",".",".",".","\u2680","\u2680","\u2680",".",".",".",".",".",".",".",".",".","\u2680"},                
                {"\u2680",".",".",".",".",".",".",".",".",".","\u2680",".",".",".",".",".",".",".",".",".",".","\u2680","\u2680",".",".",".",".",".","\u2680",".",".",".",".",".",".","\u2680"},                
                {"\u2680",".",".",".",".",".",".",".",".",".","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680",".","\u2680","\u2680","\u2680",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","\u2680"},
                {"\u2680",".",".",".",".",".",".",".","\u2680","\u2680","\u2680",".",".",".",".",".",".",".",".",".",".",".","\u2680",".",".",".",".",".",".",".",".",".","\u2680","\u2680",".","\u2680"},
                {"\u2680",".",".","\u2680","\u2680",".",".",".",".",".",".",".",".",".",".","\u2680",".",".",".","\u2680","\u2680","\u2680","\u2680",".",".",".",".",".",".",".",".","\u2680",".","\u2680",".","\u2680"},                
                {"\u2680",".",".","\u2680","\u2680",".",".",".","\u2680","\u2680",".",".",".","\u2680",".",".",".",".",".",".","\u2680",".",".",".",".",".",".",".",".",".",".",".",".","\u2680",".","\u2680"},                
                {"\u2680",".",".",".",".",".",".",".","\u2680","\u2680",".",".",".",".",".","\u2680",".",".",".","\u2680",".",".",".",".",".",".",".",".",".",".",".",".",".","\u2680",".","\u2680"},
                {"\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","\u2680","W","\u2680"},
        
            };
            return map;
        }

        static void displayMap(string[,] map, int[] player) 
        {
            for(int i = 0; i < 12; i++) {
                for(int j = 0; j < 36; j++) {
                    if(i == player[0] && j == player[1]) {
                        map[i,j] = "\u263A";
                        Console.Write(map[i,j]);
                    } else {
                        Console.Write(map[i,j]);
                    }
                    
                }
                Console.WriteLine("");
            }
        }

        static void CheckRandomEvent(string[,] map, int[] coords)
        {
            Random rnd = new Random();
            int chance = rnd.Next(0,6);
            Random rnd2 = new Random();
            int msgChance = rnd2.Next(0,9);
            string[] events = {
                "You found a friendly Ghost, gain 1 special point",
                "You find a free sandwich!",
                "You accidently run into a miror and break it, bad luck for you",
                "You stumbled across a full bowl of Holloween candy left by Anne, say thanks to her",
                "Find some enriched uranium and shove it in your proton pack.",
                "You see a blue sequin tuxedo jacket draped over a chair. It's so tacky! You can't look directly at it....",
                "A spirit asks 'are you a god?' You say no like an idiot.",
                "You see Slimer eating and entire ham. He sees you coming and drops the ham as he floats away. You pick up the ham and take a bite for science.",
                "You slip on some slime and fall on your ass. How embarrassing.",
            };
            if(chance == 5) {
                Console.WriteLine($"{events[msgChance]}");
            }
        }

        static int[] movePlayer(int[] coords, string dir, string[,] map)
        {
            int[] didWin = {99,99};
            bool won = false;
            if(dir == "w") {
                if(map[coords[0]-1,coords[1]] != "\u2680"){
                    CheckRandomEvent(map, coords);
                    int[] arr = {coords[0]-1, coords[1]};
                    won = checkWin(map, arr);
                    coords[0]--;
                    if(won) {
                        return didWin ;
                    }
                }
            } else if (dir == "a") {
                if(map[coords[0],coords[1]-1] != "\u2680"){
                    CheckRandomEvent(map, coords);
                    int[] arr = {coords[0], coords[1]-1};
                    won = checkWin(map, arr);
                    coords[1]--;
                    if(won) {
                        return didWin ;
                    }
                }
            } else if (dir == "s") {
                if(map[coords[0]+1,coords[1]] != "\u2680"){
                    CheckRandomEvent(map, coords);
                    int[] arr = {coords[0]+1, coords[1]};
                    won = checkWin(map, arr);
                    coords[0]++;
                    if(won) {
                        return didWin ;
                    }
                }
            }else if (dir == "d") {
                if(map[coords[0],coords[1]+1] != "\u2680"){
                    CheckRandomEvent(map, coords);
                    int[] arr = {coords[0], coords[1]+1};
                    won = checkWin(map, arr);
                    coords[1]++;
                    if(won) {
                        return didWin ;
                    }
                }
            }
            return coords;
        }

        static bool checkWin(string[,] map, int[] coords) {
            if(map[coords[0],coords[1]] == "W") {
                
                return true;
            }
            return false;
        }

        static string[,] updateMap(string[,] map, int[] prevLoc, int[] coords) 
        {
            string [,] newMap = new string[12,36];
            for(int i = 0; i < 12; i++) {
                for(int j = 0; j < 36; j++) {
                    if(i == coords[0] && j == coords[1]) {
                        newMap[coords[0],coords[1]] = "o";
                        //Console.Write(newMap[i,j]);
                    } else if(i == prevLoc[0] && j == prevLoc[1]){
                        newMap[prevLoc[0],prevLoc[1]] = ".";
                        //Console.Write(newMap[prevLoc[0],prevLoc[1]]);
                    } else {
                        newMap[i,j] = map[i,j];
                        //newMap[i,j] = ".";
                        //Console.Write(newMap[i,j]);
                    }
                }
                //Console.WriteLine("");
            }
            return newMap;
        }

        static Human pickClass()
        {
            Console.WriteLine("Who's Calling?");
            string playerName = Console.ReadLine();
            Console.WriteLine("Who you gunna call?");
            Console.WriteLine($"Hi, {playerName}. Choose your Ghostbuster!\n1. Ray(Healer)\n2. Egon(Attacker)\n3. Peter(Defender)");
            string playerType = Console.ReadLine();

            if(playerType == "1") {
                Healer player = new Healer($"{playerName}");
                //Console.WriteLine($"{player.Name}");
                return player;
            } else if(playerType == "2") {
                Attacker player = new Attacker($"{playerName}");
                //Console.WriteLine($"{player.Name}");
                return player;
            } else if(playerType == "3") {
                Defender player = new Defender($"{playerName}");
                //Console.WriteLine($"{player.Name}");
                return player;
            } else {
                Console.WriteLine("Please input 1, 2, or 3.");
            }
            return null;
        }

        static Human Encounter(Human player, Enemy enemy)
        {
            Console.WriteLine($"{player.Name} has encountered a level {enemy.Level} {enemy.type}!");
            Console.WriteLine($"Press 'X' To Run Away");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{player.Name}'s Health: {player.Health} ||||| {enemy.type}'s Health: {enemy.Health}");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            while(player.Health > 0 && enemy.Health > 0) {
                Console.WriteLine($"What is your move?");
                for(int i = 1; i <= player.moveSet.Count; i++) {
                    Console.WriteLine($"{i}. {player.moveSet[i-1]}");
                }
                string playerAttack = Console.ReadLine();
                if(playerAttack == "1") {
                    player.Attack(enemy);
                } else if(playerAttack == "2") {
                    if(player.Guild == "Ray") {
                        player.Heal(player);
                    } else if(player.Guild == "Egon") {
                        player.Steal(enemy);
                    } else if(player.Guild == "Peter") {
                        player.Meditate();
                    }
                } else if(playerAttack == "3") {
                    if(player.Guild == "Ray") {
                        player.Shield(enemy);
                    } else if(player.Guild == "Peter") {
                        player.Shield(enemy);
                    } else if(player.Guild == "Egon") {
                        player.CrossTheStreams(enemy);
                    }
                } else if(playerAttack == "4") {
                    if(player.Guild == "Ray") {
                        player.GhostTrap(enemy);
                    } else if(player.Guild == "Peter") {
                        player.ManEatingToaster(enemy, player);
                    }
                } else if(playerAttack =="x"  ||  playerAttack =="X"){
                    break;
                }
                enemy.Attack(player);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player.Name}'s Health: {player.Health} ||||| {enemy.type}'s Health: {enemy.Health}");
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkGray;   
            }
            Console.ForegroundColor = ConsoleColor.Red;
            if(enemy.Health <= 0) {
                Console.WriteLine("You killed the enemy!");
            } else if(player.Health <= 0) {
                Console.WriteLine("Oh dear, you are dead...");
            } else {
                Console.WriteLine("You successfully escaped!!");
            }
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkGray; 
            return player;
        }
    }
}
