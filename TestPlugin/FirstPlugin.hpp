//
//  FirstPlugin.hpp
//  TestPlugin
//
//  Created by Hyunwoo Kim on 2021/03/14.
//

#ifndef FirstPlugin_hpp
#define FirstPlugin_hpp

#include <stdio.h>

extern "C" {
const char* SayHello();
int AddTwoIntegers(int, int);
float AddTwoFloats(float, float);
}

#endif /* FirstPlugin_hpp */
