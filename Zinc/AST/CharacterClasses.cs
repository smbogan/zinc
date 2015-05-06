﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.AST
{
    public static class CharacterClasses
    {
        public static bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\r' || c == '\n' || c == '\t'; 
        }

        public static bool IsOperator(char c)
        {
            switch(c)
            {
                case '~':
                case '!':
                case '@':
                case '$':
                case '%':
                case '^':
                case '&':
                case '*':
                case '-':
                case '+':
                case '=':
                case ':':
                case '<':
                case '>':
                case ',':
                case '.':
                case '?':
                case '/':
                case '|':
                case '\\':
                    return true;
                default:
                    return false;

            }
        }

        public static bool IsQuoteStarter(char c)
        {
            switch(c)
            {
                case '`':
                case '"':
                case '\'':
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsCommentStarter(char c)
        {
            switch(c)
            {
                case '#':
                case '/':  //Note that this returns true for operators also!
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsIdentifier(char c)
        {
            return !IsWhitespace(c) && !IsOperator(c) && !IsCommentStarter(c) && !IsQuoteStarter(c);
        }
    }
}
