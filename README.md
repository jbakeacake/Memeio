# Meme.io: Social Media Application
**Contributors:**<br/>
- Jacob Baker<br/>

A simple single-page app to view and rate a collection of memes. Introduction into .NET and Angular frameworks, security (Auth0/JWT), and building an extensive API.

This is an that allows users to join a community where users can interact with each other and their posts. Users can archive each other's photos, like or dislike a photo, and leave a comment on a photo. These posts consist of pictures or memes that a user uploads to be viewed in the main gallery -- a deck of cards style of viewing pictures (think Tinder or IFunny). 

## User Stories

- [x] User can register and login
- [x] User can load a gallery of photos (stylized like a deck of cards)
- [x] User has their own profile where they can view their posts, archived photos, and their own comment section
- [x] User can upload a photo from their profile page
- [x] User can comment on other people's profile
- [ ] User can edit their profile picture
- [ ] User can search for another user via a search bar located on the nav bar
- [ ] User can enlarge photos on their profile portfolio and view comments, likes, and dislikes of these pictures
- [ ] User is notified when they receive a new comment on their profile

## Functional Stories
- [x] Store the actual photos (.png, .jpg, etc.) in a Cloud-Based Storage (e.g. Cloudinary)
- [x] Store the Urls of each photo on a database
  - [ ] Migrate to MySQL database
- [ ] UI is optimized for Phone screens

## Bonus Features

- [ ] Before a photo is uploaded, the User can resize/transform the photo
- [ ] User can view a statistical chart describing a like to dislike ratio
- [ ] User can download their portfolio

## Technicals

- Angular 9 (CLI)
- .NET 3.1
- JWT Authorization
- Cloudinary Media Storage
- SQLite for early development, MySQL for production

## Useful Links and Resources

General:<br/>
[Trello Board](https://trello.com/b/U7dajjQ9/memeio)

