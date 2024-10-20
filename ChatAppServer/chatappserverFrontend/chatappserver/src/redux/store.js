import { configureStore, combineReducers } from '@reduxjs/toolkit'
import { persistStore, persistReducer } from "redux-persist"
import storage from "redux-persist/lib/storage"
import userSlice from './slices/userSlice'
import messageSlice from "./slices/messageSlice"
import contactSlice from './slices/Contacts'



const rootReducer = combineReducers({
    user: userSlice,
    message: messageSlice,
    contact: contactSlice

})

const persistConfig = {
    key: "root",
    storage,
    version: 1,
}

const persistedReducer = persistReducer(persistConfig, rootReducer)


export const store = configureStore({
    reducer: persistedReducer,
    middleware: (getDefaultMiddleWare) => getDefaultMiddleWare({
        serializableCheck: false
    })
})

export const persistor = persistStore(store)