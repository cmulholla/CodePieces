// fractals3.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <SFML/Graphics.hpp>
#include <SFML/Window.hpp>
#include <stdlib.h>
#include <Windows.h>
#include <ctime>
#define PI 3.14159265

using namespace std;

sf::Image averagePixel(sf::Image image, float factor) {
    sf::Image outImage;
    int xSize = image.getSize().x;
    int ySize = image.getSize().y;

    //set the image to the average pixel in the image
    int red = 0, green = 0, blue = 0;
    for (int i = 0; i < xSize - 1; i++) {
        for (int j = 0; j < ySize - 1; j++) {
            red += image.getPixel(i, j).r;
            green += image.getPixel(i, j).g;
            blue += image.getPixel(i, j).b;
        }
    }
    sf::Image outimage;
    float imageM = 2;
    outimage.create(xSize / factor, ySize / factor, sf::Color(red / (xSize * ySize) * imageM, green / (xSize * ySize) * imageM, blue / (xSize * ySize) * imageM));
    return outimage;
}

sf::Image createJulia(double c, double ii, int xSize, int ySize) {
    float factor = 5;
    xSize *= factor;
    ySize *= factor;
    //julia set stuff
    //choose R > 0 such that R**2 - R >= sqrt(cx**2 + cy**2)
    int iter = 0, maxIter = 30;
    double zx, zy, xtemp, R = 2, cx = c, cy = ii; //-0.7, 0.27015 are cool coords to calculate
    sf::Image image;
    image.create(xSize, ySize, sf::Color(0, 0, 0));

    for (int i = 0; i < xSize; i++) {
        for (int j = 0; j < ySize; j++) {
            //(x, y) = (i, j)
            //scale x, y around (600, 400) from -R to R
            zx = 1.5 * (i - xSize / 2.0) / (xSize * 0.5);
            zy = 1.0 * (j - ySize / 2.0) / (ySize * 0.5);
            iter = 0;

            while ((zx * zx) + (zy * zy) < pow(R, 2) && iter < maxIter) {
                xtemp = zx * zx - zy * zy;
                zy = 2 * zx * zy + cy;
                zx = xtemp + cx;

                iter++;
            }

            //set the pixel on a greyscale gradient, with 0 iterations being white and maxIter being black
            if (iter == maxIter) {
                image.setPixel(i, j, sf::Color::Black);
            }
            else {
                image.setPixel(i, j, sf::Color(((iter * 1.f) / maxIter) * 255, ((iter * 1.f) / maxIter) * 255, ((iter * 1.f) / maxIter) * 255, 255));
            }
        }
    }

    //if the average pixel isn't taken, set the factor to 1 pls
    image = averagePixel(image, factor);
    return image;
}

int main()
{
    float xamtImg = 800;
    float yamtImg = 600;
    sf::RenderWindow window(sf::VideoMode(xamtImg, yamtImg), "SFML works!");
    window.setVerticalSyncEnabled(true);
    srand(time(NULL));
    sf::Image image;
    sf::Image mainImage;
    mainImage.create(window.getSize().x, window.getSize().y, sf::Color(0, 0, 0));
    sf::Sprite sImage;
    sf::Texture tImage;

    /*sf::Vector2f linePos(600, 400);
    int rotation = 0;
    imLine line(32, rotation);*/

    //getting the right coords to calculate the mandelbrot set
    double x, y;
    x = -1.5;
    y = 1;
    int juliapic = 0; //this is to keep track of the amount of julia sets programmed
    //amount of coords to add to calculate the next julia set
    float xAdd = 2.5 / xamtImg;
    float yAdd = 2.f / yamtImg;

    time_t t; //for file naming purposes
    bool drawFlag = false;

    while (window.isOpen())
    {
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        //calculate the julia
        if (drawFlag == false) {
            for (y = 1; y >= -1; y -= yAdd) {
                //cout << "Drawn: " << x << ", " << y << " of the Mandelbrot set.\n";
                image.create(mainImage.getSize().x / xamtImg, mainImage.getSize().y / yamtImg, sf::Color(0, 0, 0));
                image = createJulia(x, y, image.getSize().x, image.getSize().y);
                //drawLine(image, line, linePos);
                mainImage.copy(image, (x + 1.5) * (image.getSize().x / xAdd), (y + 1) * (image.getSize().y / yAdd));
            }
            if (int(100 * (x + 1.5) / 2.5) - (100 * (x + 1.5) / 2.5) > -0.001)
                cout << 100 * (x + 1.5) / 2.5 << "%\n";
        }
        if (x >= 1 && drawFlag == false) {
            mainImage.saveToFile(("Mandeljulia" + to_string(time(&t)) + ".jpg"));
            cout << "saved\n";
            drawFlag = true;
        }
        else if (drawFlag == false)
            x += xAdd;
        else {
            tImage.loadFromImage(mainImage);
            sImage.setTexture(tImage);
            sImage.setScale(window.getSize().x / mainImage.getSize().x, window.getSize().y / mainImage.getSize().y);
            window.draw(sImage);
            window.display();
        }
    }
    return 0;
}
