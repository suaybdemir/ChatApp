import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from "axios"

const initialState = {
    contacts: null,
}

export const getContacts = createAsyncThunk(
    'contact/getContact',
    async () => {
        const response = await axios.get("https://localhost:44335/api/Users/GetConnectedUsersAll")
        return response.data
    },
)



export const contactSlice = createSlice({
    name: 'contact',
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder.addCase(getContacts.fulfilled, (state, action) => {
            state.contacts = action.payload
        })

    },
})

// Action creators are generated for each case reducer function
export const { } = contactSlice.actions

export default contactSlice.reducer