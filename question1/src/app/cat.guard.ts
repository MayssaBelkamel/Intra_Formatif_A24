import { inject } from '@angular/core';
import { CanActivateFn, createUrlTreeFromSnapshot } from '@angular/router';
import { UserService } from '../app/user.service';

export const catGuard: CanActivateFn = (route, state) => {
  if (inject(UserService).currentUser?.prefercat==false)
     // S'il n'est pas connecté on le redirige vers la page de login
     return createUrlTreeFromSnapshot(route, ["/dog"]);
   // S'il est connecté, tout est beau on continue!
   else return true;
};
