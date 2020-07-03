const isDev = process.env.NODE_ENV !== "production"


export let serverURL = isDev ? "http://localhost:4002" : "http://elyspio.fr/global/android-desktop-manager";
export const expressPort = 4001;
