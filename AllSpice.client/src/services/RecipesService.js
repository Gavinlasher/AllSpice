import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class RecipesService {
  async getAll() {
    const res = await api.get("api/recipes")
    logger.log(res.data)
    AppState.recipes = res.data
  }
  async createRecipe(recipeData) {
    const res = await api.post("api/recipes", recipeData)
    logger.log(res.data)
    AppState.recipes = [...AppState.recipes, res.data]
  }
  async remove(id) {
    const res = await api.delete("api/recipes/" + id)
    logger.log(res.data)
    AppState.recipes = AppState.recipes.filter(r => r.id != id)
  }
  async favorite(id) {
    const res = await api.post("api/favorites", id)
    logger.log(res.data)
    AppState.favorites = [...AppState.favorites, res.data]
  }
}

export const recipesService = new RecipesService()