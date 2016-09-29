import { GoFishUIAngular2Page } from './app.po';

describe('go-fish-ui-angular2 App', function() {
  let page: GoFishUIAngular2Page;

  beforeEach(() => {
    page = new GoFishUIAngular2Page();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
