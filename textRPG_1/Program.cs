using System;
using System.Numerics;
using System.Threading.Channels;
using System.Xml.Serialization;

namespace textRPG_1
{
    // 객체(OOP Object Oriented Programming) 지향

    class Program
    {
        enum ClassType
        {
            None = 0,
            Knight = 1,
            Archer = 2,
            Mage = 3
        }

        struct Player //구조체 
        {
            public int hp;
            public int attack;
        }

        enum MonsterType
        {
            None = 0,
            Slime = 1,
            Orc = 2,
            Skeleton = 3
        }

        struct Monster //구조체 
        {
            public int hp;
            public int attack;
        }

        static ClassType ChooseClass()
        {
            Console.WriteLine("직업을 선택하세요!");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 법사");

            ClassType Choice = ClassType.None;
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Choice = ClassType.Knight;
                    break;
                case "2":
                    Choice = ClassType.Archer;
                    break;
                case "3":
                    Choice = ClassType.Mage;
                    break;
            }

            return Choice;
        }

        static void CreatePlayer(ClassType Choice, out Player player)
        {
            switch (Choice) // 기사(100/10), 궁수(75/12), 법사(50/15)
            {
                case ClassType.Knight:
                    player.hp = 100;
                    player.attack = 10;
                    break;
                case ClassType.Archer:
                    player.hp = 75;
                    player.attack = 12;
                    break;
                case ClassType.Mage:
                    player.hp = 50;
                    player.attack = 15;
                    break;
                default:
                    player.hp = 0;
                    player.attack = 0;
                    break;
            }
        }

        static void CreateRandomMonster(out Monster monster)
        {
            //랜덤으로 1~3 몬스터 중 하나를 리스폰 
            Random rand = new Random();
            int randMonster = rand.Next(1, 4); // (1, 4) 1-포함 4-제외 
            switch (randMonster)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine("슬라임이 스폰되었습니다!");
                    monster.hp = 20;
                    monster.attack = 2;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine("오크가 스폰되었습니다!");
                    monster.hp = 40;
                    monster.attack = 4;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine("스켈레톤이 스폰되었습니다!");
                    monster.hp = 30;
                    monster.attack = 3;
                    break;
                default:
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }
        static void Fight(ref Player player, ref Monster monster) //쌈박질 시키는 코드 
        {
            while (true)
            {
                //플레이가 몬스터 공격
                monster.hp -= player.attack;
                if (monster.hp <= 0)
                {
                    Console.WriteLine("승리했습니다!");
                    Console.WriteLine($"남은 체력 : {player.hp}");
                    break;
                }

                //몬스터 반격
                player.hp -= monster.attack;
                if (player.hp <= 0)
                {
                    Console.WriteLine("패배했습니다!");
                    break;
                }
            }
        }
        static void EnterField(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("필드에 접속했습니다!");

                // 몬스터 생성 랜덤으로 1~3 몬스터 중 하나를 리스폰 
                Monster monster;
                CreateRandomMonster(out monster);

                //[1] 전투모드 돌입 
                //[2] 일정 확률로 마을로 도망 
                Console.WriteLine("[1] 전투 모드로 돌입!");
                Console.WriteLine("[2] 일정 확률로 마을로 도망");

                string input = Console.ReadLine();
                if (input == "1") //전투 모드 
                {
                    Fight(ref player, ref monster);
                }
                else if (input == "2") // 마을로 도망 
                {
                    //33%
                    Random rand = new Random();
                    rand.Next(0, 101);
                    int randValue = rand.Next(0, 101);
                    if (randValue <= 33)
                    {
                        Console.WriteLine("도망치는데 성공했습니다!");
                        break;
                    }
                    else
                    {
                        Fight(ref player, ref monster);
                    }
                }
            }
        }
        static void EnterGame(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("마을에 접속했습니다!");
                Console.WriteLine("[1] 필드로 간다!");
                Console.WriteLine("[2] 로비로 돌아가기!");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    EnterField(ref player);
                }
                else if (input == "2")
                {
                    break;
                }
                /**
                switch (input) //switch 방법 break는 스위치문에서만 빠져나감으로 retuen을 줌으로 로비로 돌아가기
                {
                    case "1":
                        //EnterField();
                        break;
                    case "2":
                        return;
                }
                **/
            }

        }

        /** 절차(procedure) 지향 -- 함수 기반으로 만드는 것
         * 함수의 호출순서가 중요함 (간단한 프로그램이나 툴 만들 때 사용)
         * 장점 - 굉장히 심플하고 직관적이라는 점 
         * 단점 - 프로그램이 비대해지고 복잡해질 수록 유지보수가 힘들고 로직이 꼬일 수 있다. **/
        static void Main(string[] agrs)
        {
            while (true)
            {
                ClassType Choice = ChooseClass();
                if (Choice != ClassType.None)
                {
                    // 캐릭터 생성 
                    Player player;
                    CreatePlayer(Choice, out player);

                    EnterGame(ref player);
                }
            }
        }
    }
}
