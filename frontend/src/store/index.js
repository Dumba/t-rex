import { createStore } from 'vuex'

export default createStore({
  state: {
    displayPage: "main",
    theme: "light"
  },
  mutations: {
    changeDisplayPage(state, newPage) {
      state.displayPage = newPage;
    },
    switchTheme(state, newTheme) {
      state.theme = newTheme;
      document.documentElement.setAttribute('theme', newTheme);
    }
  },
  actions: {
  },
  modules: {
  }
})
