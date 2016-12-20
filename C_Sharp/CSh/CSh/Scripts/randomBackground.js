let images = [
    "/Content/Images/Background1.jpg",
    "/Content/Images/Background2.jpg",
    "/Content/Images/Background3.jpg",
    "/Content/Images/Background8.jpg",
    "/Content/Images/Background6.jpg",
    "/Content/Images/Background5.jpg",
    "/Content/Images/Background4.jpg",
    "/Content/Images/Background7.jpg",
    "/Content/Images/Background9.jpg",
    "/Content/Images/Background10.jpg",
    "/Content/Images/Background11.jpg"
];

let randomImageUrl = images[Math.round(Math.random() * images.length - 1)];



$('body').css({
    'background-image': `url(${randomImageUrl})`
});