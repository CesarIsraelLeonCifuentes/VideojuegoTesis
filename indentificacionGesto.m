clc
clear all


while 1
  prompt = 'Ingrese el valor del gesto ';
  x = input(prompt,"s")
  enviarGesto(x)
    
end


function x = enviarGesto(x)
    tcpipClient = tcpip('127.0.0.1',55001,'NetworkRole','Client');
    set(tcpipClient,'Timeout',30);
    fopen(tcpipClient);
    fwrite(tcpipClient,x);
    disp('Dato enviado');
    fclose(tcpipClient);
end


