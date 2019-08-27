namespace csharp thrift.demo

struct Pt {
 1: double x
 2: double y
}

struct Kevp {
 1: string name
 2: i32 id
 3: i64 lid
 4: map<string,string> dict
 5: Pt pt1
}

service MyDemo {
 i32 testM1(1:i32 num1, 2:i32 num2)
 list<string> testM2(1:string s1)
 void testM3(1:map<string,string> dict1)
 void testM4(1:list<Kevp> kvp)
}