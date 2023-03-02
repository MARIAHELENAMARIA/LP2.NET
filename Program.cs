// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var idade = 66;
int mes = 11;
int numero = -1;

Console.WriteLine("A data corrente eh " + DateTime.Now);

if (idade >= 18)
    Console.WriteLine("voce eh maior de idade");
else
    Console.WriteLine("voce eh menor de idade");

switch(mes){
    case 1:
        Console.WriteLine("JANEIRO");    
        break;
    case 2:
        Console.WriteLine("FEVEREIRO");
        break;
    case 3:
        Console.WriteLine("MARÇO");
        break;
    case 4:
        Console.WriteLine("ABRIL");
        break;    
    case 5:
        Console.WriteLine("MAIO");
        break;
    case 6:
        Console.WriteLine("JUNHO");
        break;
    case 7:
        Console.WriteLine("JULHO");
        break;
    case 8:
        Console.WriteLine("AGOSTO");
        break;
    case 9:
        Console.WriteLine("STEMBRO");
        break;
    case 10:
        Console.WriteLine("OUTUBRO");
        break;
    case 11:
        Console.WriteLine("NOVEMBRO");
        break;
    case 12:
        Console.WriteLine("DEZEMBRO");
        break;
    default:
        Console.WriteLine("OPCAO INVALIDA");
        break;                                
}    

while(numero!=10){

    numero = Convert.ToInt32(Console.ReadLine());

    if(numero<10){
        Console.WriteLine("tente um numero maior");
    }
    else if(numero>10){
        Console.WriteLine("tente um numero menor");
    }
    else{
        Console.WriteLine("voce acertou!!!");
    }
}