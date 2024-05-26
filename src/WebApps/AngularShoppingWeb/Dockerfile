FROM node:18-alpine as build

WORKDIR /app/src
COPY package.json ./
RUN npm i
COPY . .
RUN npm run build

FROM node:18-alpine
WORKDIR /use/app
COPY --from=build /app/src/dist/AngularShoppingWeb ./
CMD node ./server/server.mjs

EXPOSE 4000
